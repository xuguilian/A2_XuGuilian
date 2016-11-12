using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_XuGuilian
{
    class Program
    {
        //Declare the arrays in the class level seems better. If we put them into every function, it will repeat a few times.
        //Plus I don't know how to return two result arrays from one function.
        public static double[] studentScore = new double[8];
        public static char[] grades = new char[8];
        public static double score_average, score_std_dev, score_median; 

        public static void ComputeGrades(ref char[,] answerByStudent, ref char[] answerKey, ref int[] scoreOfEachQuestion)
        {
            int num_students = 8; // answerByStudent.GetLength(0);
            const int num_questions = 10; // answerByStudent.GetLength(1);
            for (int idx_stu=0; idx_stu < num_students; idx_stu++)
           {
                for(int idx_quest=0; idx_quest < num_questions; idx_quest++)
                {
                    if (answerByStudent[idx_stu, idx_quest] == answerKey[idx_quest])
                        studentScore[idx_stu] += scoreOfEachQuestion[idx_quest];
                }

                double score_percentage = studentScore[idx_stu] / scoreOfEachQuestion.Sum();
                if (score_percentage >= 0.9)
                {
                    grades[idx_stu] = 'A';
                }
                else if(score_percentage >= 0.8)
                {
                    grades[idx_stu] = 'B';
                }
                else if (score_percentage >= 0.7)
                {
                    grades[idx_stu] = 'C';
                }
                else if (score_percentage >= 0.6)
                {
                    grades[idx_stu] = 'D';
                }
                else
                {
                    grades[idx_stu] = 'F';
                }
           }
        }

        public static double variance(double[] nums)
        {
            if (nums.Length > 1)
            {
                // Get the average of the values
                double avg = getAverage(nums);

                // Now figure out how far each point is from the mean
                // So we subtract from the number the average
                // Then raise it to the power of 2
                double sumOfSquares = 0.0;

                foreach (int num in nums)
                {
                    sumOfSquares += Math.Pow((num - avg), 2.0);
                }

                // Finally divide it by n - 1 (for standard deviation variance)
                // Or use length without subtracting one ( for population standard deviation variance)
                return sumOfSquares / (double)(nums.Length - 1);
            }
            else { return 0.0; }
        }

        // Square root the variance to get the standard deviation
        public static double standardDeviation(double variance)
        {
            return Math.Sqrt(variance);
        }

        // Get the average of our values in the array
        public static double getAverage(double[] nums)
        {
            int sum = 0;

            if (nums.Length > 1)
            {

                // Sum up the values
                foreach (int num in nums)
                {
                    sum += num;
                }

                // Divide by the number of values
                return sum / (double)nums.Length;
            }
            else { return (double)nums[0]; }
        }
        
        public static void ComputeStatistics()
        {
            // Get the variance of our values
            double varianceValue = variance(studentScore);
            // Now calculate the standard deviation 
            score_std_dev = standardDeviation(varianceValue);
            //Get score average
            score_average = getAverage(studentScore);

            double[] studentScore_tmp = (double[])studentScore.Clone();
            Array.Sort(studentScore_tmp);
            //compute the median
            int size = studentScore_tmp.Length;
            int mid = size / 2;
            score_median = (size % 2 != 0) ? studentScore_tmp[mid] : (studentScore_tmp[mid] + studentScore_tmp[mid - 1]) / 2;
        }

        public static void DisplayResults(string[] names)
        {
            Console.WriteLine("{0,-10} {1,-10} {2,-10}", "Student", "Score", "Grade");
            for (int idx_stu = 0; idx_stu < names.Length; idx_stu++)
                Console.WriteLine("{0,-10} {1,-10} {2,-10}", names[idx_stu], studentScore[idx_stu], grades[idx_stu]);

            Console.WriteLine();
            Console.WriteLine();

            Console.Write("{0,-15}","Average:");
            Console.WriteLine("{0:N2}", score_average);

            Console.Write("{0,-15}", "Std. Dev.:");
            Console.WriteLine("{0:N2}", score_std_dev);

            Console.Write("{0,-15}", "Median:");
            Console.WriteLine("{0:N2}", score_median);

            Console.WriteLine();
            Console.WriteLine();

            DrawHistogram();
        }

        public static void DrawHistogram()
        {
            char[] letters = { 'A', 'B', 'C', 'D', 'F' };
            foreach (char value in letters)
            {
                Console.Write(value + " |");
                for (int i = 0; i < grades.Length; i++)
                {
                    if (grades[i] == value)
                        Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public static void Main(string[] args)
        {
            string[] names = { "Jack", "John", "Jill", "Mary", "Peter", "Bob", "Nancy", "Pat" };
            char[] answerKey = { 'D', 'B', 'D', 'C', 'C', 'D', 'A', 'E', 'A', 'D' };
            int[] scoreOfEachQuestion = { 2, 2, 5, 3, 3, 4, 4, 4, 6, 7 };

            char[,] answerByStudent = {
                { 'A','B','A','C','C','D','E','E','A','D'},
                { 'D','B','A','B','C','A','E','E','A','D' },
                { 'E','D','D','A','C','B','E','E','A','D' },
                { 'C','B','A','E','C','C','E','E','A','D' },
                { 'A','B','D','C','C','D','E','E','A','D' },
                { 'B','B','E','C','C','D','E','E','A','D' },
                { 'B','B','A','C','C','D','E','E','A','D' },
                { 'E','B','E','C','C','D','E','E','A','D' }
                };

            ComputeGrades(ref answerByStudent, ref answerKey, ref scoreOfEachQuestion);
            ComputeStatistics();
            DisplayResults(names);
            Console.ReadLine();
        }
    }
}