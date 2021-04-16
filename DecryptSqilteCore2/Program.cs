using System;

namespace DecryptSqilteCore2
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataSqilte
            var result1 = new DataSqilteMethod().GetUser();
            //SQLCipher
            var result2 = new SQLCipherMethod().GetUser();
            //sqlsugar
            var result3 = new SqlSugarMethod().GetUser();
        }

        
    }
}
