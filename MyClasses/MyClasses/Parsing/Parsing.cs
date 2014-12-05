namespace MyClasses
{
    using System;
    using System.IO;
    using System.Text;

    public static class Parsing
    {
        /// <summary>
        /// The error messages for <see cref="LastErrorMessage"/>.
        /// </summary>
        private static string[] errorMessages = 
        {
            "none", "wrong input", "no input at all", 
            "value is too large", "unknown exception"
        };

        /// <summary>
        /// Saves information for <see cref="LastErrorMessage"/>
        /// </summary>
        private static int errorCode = 0;

        /// <summary>
        /// This need for <see cref="ReleaseOutput"/>
        /// </summary>
        private static TextWriter old = Console.Out;

        /// <summary>
        /// Gets the last error message.
        /// </summary>
        /// <value>
        /// The last error message.
        /// </value>
        public static string LastErrorMessage
        {
            get { return errorMessages[errorCode]; }
        }

        /// <summary>
        /// Parses the one integer from console arguments and provides interface
        /// to handle errors <see cref="LastErrorMessage"/>.
        /// </summary>
        /// <returns>
        /// The one integer from console arguments.
        /// </returns>
        /// <param name='args'>
        /// Command line arguments string array.
        /// </param>
        public static int ParseOneIntegerFromConsoleArgs(string[] args)
        {
            try
            {
                errorCode = 0;
                return Convert.ToInt32(args[0]);
            }
            catch (FormatException)
            {
                errorCode = 1; 
                return 0;
            }
            catch (IndexOutOfRangeException)
            {
                errorCode = 2;  
                return 0;
            }
            catch (NullReferenceException)
            {
                errorCode = 2; 
                return 0;
            }
            catch (OverflowException)
            {
                errorCode = 3; 
                return 0;
            }
            catch (Exception e)
            {
                errorCode = 4;                 
                errorMessages[4] = e.Message;
                return 0;
            }           
        }

        /// <summary>
        /// Captures the data printed by Console.* until ReleaseOutput executed.
        /// </summary>
        /// <returns>
        /// The StringBuilder where printed data will be sent
        /// </returns>
        public static StringBuilder CaptureOutput()
        {
            var output = new StringBuilder();
            var sw = new StringWriter(output);
            Console.SetOut(sw);
            return output;
        }

        /// <summary>
        /// Sets output to Console
        /// </summary>
        public static void ReleaseOutput()
        {
            Console.SetOut(old);
        }

        /// <summary>
        /// Reads the integer matrix from file, uses space 
        /// as separator of values.
        /// </summary>
        /// <returns>
        /// The 2D array.
        /// </returns>
        /// <param name='path'>
        /// Path to file.
        /// </param>
        public static int[][] ReadIntegerMatrixFromFile(string path)
        {
            try
            {
                string[] data = File.ReadAllLines(path);
                char[] separators = { ' ', '\n' };
                int rowNumber = Convert.ToInt32(data[0].Split(separators)[0]);
                int columnNumber = Convert.ToInt32(data[0].Split(separators)[1]);
                int[][] values = new int[rowNumber][];
                for (int i = 0; i < rowNumber; i++)
                {
                    values[i] = new int[columnNumber];
                }

                for (int i = 0; i < rowNumber; i++)
                {
                    for (int j = 0; j < columnNumber; j++)
                    {
                        values[i][j] = Convert.ToInt32(data[i + 1].Split(separators)[j]);
                    }
                }

                return values;
            } 
            catch
            {
                throw new IOException("Incorrect input file");
            }
        }

        /// <summary>
        /// Writes the integer matrix to file.
        /// </summary>
        /// <param name='path'>
        /// Full name of the file where matrix will be written.
        /// </param>
        /// <param name='values'>
        /// Matrix to be overwritten.
        /// </param>
        public static void WriteIntegerMatrixToFile(string path, int[][] values)
        {
            try
            {
                int rowNumber = values.Length;
                int columnNumber = values[0].Length;
                StreamWriter output = new StreamWriter(path, false);
                output.WriteLine(string.Format("{0} {1}", rowNumber, columnNumber));
                for (int i = 0; i < rowNumber; i++)
                {
                    int j = 0;
                    for (j = 0; j < columnNumber - 1; j++)
                    {
                        output.Write(values[i][j]);
                        output.Write(' ');
                    }

                    output.WriteLine(values[i][j]);
                }

                output.Close();
            } 
            catch
            {
                throw new ArgumentException("Wrong data");
            }
        }
    }
}