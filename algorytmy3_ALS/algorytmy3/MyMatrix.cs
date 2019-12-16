/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 2
/// Rozwiązywanie układów równań liniowych metodą eliminacji Gaussa

using System;
using System.Collections.Generic;
using System.Linq;
using MiscUtil;
/// Library MiscUtil allows to use Operator class which provides
/// high-performance support for the common operators (+, -, *, etc) for generic types
/// more details https://jonskeet.uk/csharp/miscutil/usage/genericoperators.html

namespace algorytmy3
{
    // to solve A · X = B
    public class MyMatrix<T>
    {

        public T[,] A; //matrix A
        public T[] B;  //vector B
        public T[] X; //vector X to store a reference solution
        public T[] Xgauss; //vector X with a gauss result
        public int dimensions;
        public int[] col; // array for storing column order

        public MyMatrix(int dimensions)
        {
            this.dimensions = dimensions;
            A = new T[dimensions, dimensions];
            X = new T[dimensions];
            B = new T[dimensions];
            Xgauss = new T[dimensions];
            col = new int[dimensions];
            for (int i = 0; i < dimensions; i++)
                col[i] = i;
        }

    //NEW macierz transponowana
    public static T[,] Transpose(T[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            T[,] transposed = new T[cols, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transposed[j, i] = matrix[i, j];
                }
            }
            return transposed;
        }

        //NEW generuj nowa macierz  stworzona z kolumn macierzy bazowej
        public static T[,] GetMatrixFromOtherMatrixColumns(T[,] matrix, List<int> columns)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            T[,] M = new T[rows, columns.Count()];
            int c = 0;
            for (int j = 0; j < cols; j++)
            {
                if (columns.Contains(j))
                {
                    for (int i = 0; i < rows; i++)
                    {
                       // Console.WriteLine($" i, j : {i},{j},  c= {c}");
                        M[i, c] = matrix[i, j];
                    }
                    c += 1;
                }
            }
            return M;
        }
        public static T[,] Add(T[,] A, T[,] B)
        {
            int m = A.GetLength(0);
            int n = B.GetLength(1);
            T[,] C = new T[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = Operator.Add(A[i, j], B[i, j]);
                }
            }
            return C;
        }

        // Multipication  C = A*B
        public static T[,] Multiplication(T[,] A, T[,] B)
        {
            T zero = Operator.Subtract(A[0,0], A[0,0]);
            T[,] C = new T[A.GetLength(0), B.GetLength(1)];
            if (A.GetLength(1) == B.GetLength(0))
            {
                for (int i = 0; i < C.GetLength(0); i++)
                {
                    for (int j = 0; j < C.GetLength(1); j++)
                    {
                        C[i, j] = zero;
                        for (int k = 0; k < A.GetLength(1); k++) // OR k<b.GetLength(0)
                            C[i, j] = Operator.Add(C[i, j], Operator.Multiply(A[i, k], B[k, j]));
                    }
                }
            }
            else
            {
                Console.WriteLine("\n Number of columns in First Matrix should be equal to Number of rows in Second Matrix.");
                Console.WriteLine("\n Please re-enter correct dimensions.");
                Environment.Exit(-1);
            }
            return C;
        }
        


 

        // multiplication matrix A and vector X to get vector B
        public void ComputeB()
        {
            for (int y = 0; y < dimensions; y++)
                for (int x = 0; x < dimensions; x++)
                    B[y] = Operator.Add(B[y], Operator.Multiply(A[x, y], X[x]));
        }

        /// zeroing the matrix value in the step column below the diagonal
        public void ComputeStep(int step)
        {
            for (int n = step; n < dimensions; n++)
            {
                T div = Operator.Divide(A[col[step - 1], n], A[col[step - 1], step - 1]);
                for (int i = 0; i < dimensions; i++)
                    A[col[i], n] = Operator.Subtract(A[col[i], n], Operator.Multiply(div, A[col[i], step - 1]));

                B[n] = Operator.Subtract(B[n], Operator.Multiply(div, B[step - 1]));
            }
        }


        ///  Calculate result as Xgauss vector from step matrix with 0 under diagonal
        public void GetResult()
        {
            for (int j = dimensions - 1; j >= 0; j--)
            {
                Xgauss[col[j]] = Operator.Divide(B[j], A[col[j], j]);
                for (int i = dimensions - 1; i > j; i--)
                {
                    A[col[i], j] = Operator.Divide(A[col[i], j], A[col[j], j]);
                    Xgauss[col[j]] = Operator.Subtract(Xgauss[col[j]], Operator.Multiply(A[col[i], j], Xgauss[col[i]]));
                }
            }
        }

        //Gauss elimination with partial pivoting.
        public void ComputePG()
        {
            for (int n = 1; n < dimensions; n++)
            {
                //find the greates value from n column
                int num = n - 1;
                T max = A[col[n - 1], n - 1];
                for (int i = n - 1; i < dimensions; i++)
                {
                    T temp = Absolute(A[col[n - 1], i]); 
                    if (Operator.GreaterThan<T>(temp, max)) { max = temp; num = i; }
                }
                //move row with the greatest value in n column to the top of matrix A and vector B
                if (num != n)
                {
                    for (int x = n - 1; x < dimensions; x++)
                    {
                        T tempmaxA = A[col[x], num];
                        A[col[x], num] = A[col[x], n - 1];
                        A[col[x], n - 1] = tempmaxA;
                    }
                    T tempB = B[num];
                    B[num] = B[n - 1];
                    B[n - 1] = tempB;
                }
                ComputeStep(n);
            }
            GetResult();
        }
     
        // get the difference between reference vector X and calculated result Xgauss
        public T GetDiff()
        {
            T sum = Operator.Subtract(X[0], Xgauss[0]);
            for (int y = 0; y < dimensions; y++)
            {
                T diff = Operator.Subtract(X[y], Xgauss[y]);

                sum = Operator.Add(Absolute(sum), Absolute(diff));

                sum = Operator.Add(sum, Absolute(diff));  //14.11.2019 add absolute 
            }
            return sum;
        }
        public void SetMatrixA(int i, int j, T value) { A[i, j] = value; }
        public void SetVectorB(int j, T value) { B[j] = value; }
        public void SetVectorX(int j, T value) { X[j] = value; }

        public T Absolute(T obj)
        {
            T zero = Operator.Subtract(obj, obj);
            if (Operator.LessThan(obj, zero))
                obj = Operator.Subtract(obj, Operator.Add(obj, obj));
            return obj;
        }
    }
}
