using System.IO;

using RB.Staff.Common.Pub.Types;

namespace RB.Staff.Web.Helpers
{
    internal static class PersonSalaryReportSerializer
    {
        public static byte[] SerializeReport(
            PersonSalaryReport report )
        {
            using( var memoryStream = new MemoryStream() ) {
                using( var sw = new StreamWriter( memoryStream ) ) {
                    sw.WriteLine( GetHeaderLine() );
                    foreach( var personReportItem in report.Persons ) {
                        sw.WriteLine( GetDataLine( personReportItem ) );
                    }
                    sw.WriteLine( GetFooterLine( report ) );
                    sw.Flush();
                    return memoryStream.ToArray();
                }
            }
        }

        private static string GetFooterLine(
            PersonSalaryReport report )
        {
            return GetReportLine( string.Empty, report.TotalSalary, report.TotalTaxes, report.TotalTakeHomeSalary );
        }

        private static string GetDataLine(
            PersonReportItem personReportItem )
        {
            return GetReportLine(
                personReportItem.Name,
                personReportItem.Salary,
                personReportItem.Tax,
                personReportItem.TakeHomeSalary );
        }

        private static string GetHeaderLine()
        {
            return GetReportLine(
                "Имя",
                "Заработная плата",
                "Сумма налога на з\\п",
                "Заработная плата с вычетом налога" );
        }

        private static string GetReportLine(
            string nameColumn,
            string salaryColumn,
            string taxColumn,
            string takeHomeSalaryColumn )
        {
            return string.Format(
                GetReportLineFormat(),
                nameColumn,
                salaryColumn,
                taxColumn,
                takeHomeSalaryColumn );
        }

        private static string GetReportLineFormat()
        {
            return "{0,-30}{1,-20}{2,-20}{3,-20}";
        }

        private static string GetReportLine(
            string nameColumn,
            decimal salary,
            decimal tax,
            decimal takeHomeSalary )
        {
            return string.Format(
                GetReportLineFormat(),
                nameColumn,
                salary.ToString( "F" ),
                tax.ToString( "F" ),
                takeHomeSalary.ToString( "F" ) );
        }
    }
}