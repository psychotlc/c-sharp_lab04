using System;
using System.Collections;
using System.Collections.Generic;


namespace lab04{

    class Program{
        static void Main(){
            task2.Car[] cars = new task2.Car[]
            {
                new task2.Car("Toyota", 2020, 180),
                new task2.Car("Honda", 2019, 175),
                new task2.Car("Ford", 2022, 200),
                new task2.Car("Chevrolet", 2021, 190),
            };

            Array.Sort(cars, new task2.CarComparer("Name"));
            foreach(var car in cars){
                Console.WriteLine(car);
            }
        }
    }
    class task1{
        class MyMatrix{
            int[,] matrix;
            public int Rows{get; private set;}
            public int Columns{get; private set;}

            MyMatrix(int rows, int columns){
                Rows = rows;
                Columns = columns;
                this.matrix = new int[Rows,Columns];
                Random rdm = new Random();
                for (int i = 0; i < Rows; i++){
                    for (int j = 0; j < Columns; j++){
                        matrix[i,j] = rdm.Next();
                    }
                }

                
            }

            public int this[int row, int column]
            {
                get
                {
                    if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                    {
                        throw new IndexOutOfRangeException("Индекс за пределами матрицы");
                    }
                    return matrix[row, column];
                }
                set
                {
                    if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                    {
                        throw new IndexOutOfRangeException("Индекс за пределами матрицы");
                    }
                    matrix[row, column] = value;
                }
            }

            public static MyMatrix operator + (MyMatrix matrix1, MyMatrix matrix2){
                if (matrix1.Columns != matrix2.Columns || matrix1.Rows != matrix2.Rows){
                    throw new Exception("Матрицы должны быть одинакового размера");
                }

                MyMatrix result = new MyMatrix(matrix1.Columns, matrix1.Rows);

                for (int i = 0; i < result.Columns; i++){
                    for (int j = 0; i < result.Rows; i++){
                        result[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }

                return result;
            }

            public static MyMatrix operator - (MyMatrix matrix1, MyMatrix matrix2){
                if (matrix1.Columns != matrix2.Columns || matrix1.Rows != matrix2.Rows){
                    throw new Exception("Матрицы должны быть одинакового размера");
                }

                MyMatrix result = new MyMatrix(matrix1.Columns, matrix1.Rows);

                for (int i = 0; i < result.Columns; i++){
                    for (int j = 0; i < result.Rows; i++){
                        result[i, j] = matrix1[i, j] - matrix2[i, j];
                    }
                }

                return result;
            }

            public static MyMatrix operator *(MyMatrix matrix, int scalar)
            {
                MyMatrix result = new MyMatrix(matrix.Rows, matrix.Columns);

                for (int i = 0; i < matrix.Rows; i++)
                {
                    for (int j = 0; j < matrix.Columns; j++)
                    {
                        result[i, j] = matrix[i, j] * scalar;
                    }
                }

                return result;
            }

            public static MyMatrix operator /(MyMatrix matrix, int scalar)
            {
                if (scalar == 0)
                {
                    throw new DivideByZeroException("Деление на ноль невозможно");
                }

                MyMatrix result = new MyMatrix(matrix.Rows, matrix.Columns);

                for (int i = 0; i < matrix.Rows; i++)
                {
                    for (int j = 0; j < matrix.Columns; j++)
                    {
                        result[i, j] = matrix[i, j] / scalar;
                    }
                }

                return result;
            }
        }
    }

    public class task2{
        public class Car
        {
            public string Name { get; set; }
            public int ProductionYear { get; set; }
            public int MaxSpeed { get; set; }

            public Car(string name, int productionYear, int maxSpeed)
            {
                Name = name;
                ProductionYear = productionYear;
                MaxSpeed = maxSpeed;
            }

            public override string ToString()
            {
                return $"{Name} ({ProductionYear}) - Max Speed: {MaxSpeed} km/h";
            }
        }

        public class CarComparer : IComparer<Car>
        {
            private string sortBy;

            public CarComparer(string sortBy)
            {
                this.sortBy = sortBy;
            }

            public int Compare(Car x, Car y)
            {
                switch (sortBy)
                {
                    case "Name":
                        return string.Compare(x.Name, y.Name);
                    case "ProductionYear":
                        return x.ProductionYear - y.ProductionYear;
                    case "MaxSpeed":
                        return x.MaxSpeed - y.MaxSpeed;
                    default:
                        throw new ArgumentException("Недопустимый параметр сортировки");
                }
            }
        }


    }

    class task3{
        class Car
        {
            public string Name { get; set; }
            public int ProductionYear { get; set; }
            public int MaxSpeed { get; set; }

            public Car(string name, int productionYear, int maxSpeed)
            {
                Name = name;
                ProductionYear = productionYear;
                MaxSpeed = maxSpeed;
            }

            public override string ToString()
            {
                return $"{Name} ({ProductionYear}) - Max Speed: {MaxSpeed} km/h";
            }
        }

        class CarCatalog : IEnumerable<Car>
        {
            private Car[] cars;

            public CarCatalog(Car[] cars)
            {
                this.cars = cars;
            }

            public IEnumerator<Car> GetEnumerator()
            {
                return GetEnumeratorDirect();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<Car> GetEnumeratorDirect()
            {
                for (int i = 0; i < cars.Length; i++)
                {
                    yield return cars[i];
                }
            }

            public IEnumerator<Car> GetEnumeratorReverse()
            {
                for (int i = cars.Length - 1; i >= 0; i--)
                {
                    yield return cars[i];
                }
            }

            public IEnumerator<Car> GetEnumeratorByYear(int year)
            {
                for (int i = 0; i < cars.Length; i++)
                {
                    if (cars[i].ProductionYear == year)
                    {
                        yield return cars[i];
                    }
                }
            }

            public IEnumerator<Car> GetEnumeratorByMaxSpeed(int maxSpeed)
            {
                for (int i = 0; i < cars.Length; i++)
                {
                    if (cars[i].MaxSpeed >= maxSpeed)
                    {
                        yield return cars[i];
                    }
                }
            }
        }
    }
}