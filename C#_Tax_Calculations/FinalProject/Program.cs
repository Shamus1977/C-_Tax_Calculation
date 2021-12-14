using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalProject
{
    //--------------------------- Program --------------------------------------------------------------
    internal class Program //MAIN ENTRY & INSTRUCTIONS ---------------------------------
    {
        // --------------------- MAIN ENTRY POINT & INSTRUCTIONS BELOW ------------------------------------------------------
        static void Main(string[] args)
        {
            // INSTRUCTIONS:   THE CODE WHICH RUNS TEST VIA THE COMMAND LINE ENTRIES IS COMMENTED OUT ON THE 
            //  FIRST THREE LINES BELOW THE INSTRUCTIONS.
            //  IF THESE THREE LINES ARE LEFT COMMENTED OUT THE PROGRAM WILL RUN AND PROCESS THE EMPLOYEE INFO
            //   PROVIDED AND RETURN A LIST.
            // UPON CREATION OF EACH EMPLOYEE RECORD YOU WILL BE ASKED RATHER YOU PREFER VERBOSE OF SILENT MODE.
            //  THE OUTPUT WILL RESPOND APPROPRIATLY.
            // AFTER EACH EMPLOYEE RECOED IS PROCESSED IT IS ADDED TO A LIST.
            //  YOU WILL THEN BE ASKED HOW YOU WOULD LIKE TO SORT THE LIST, AND GIVEN INSTRUCTIONS ON THE 
            //  PROPER COMMANDS.
            // IF YOU UNCOMMENT THE FIRST THREE LINES THE COMMAND PROMP VERSION WILL RUN FIRST FOLLOWED 
            //  BY THE LIST DRIVEN VERSION.
            //
            // YOU CAN CHANGE THE CSV FOR THE TAX DATABASE IN THE TAX CALCULATOR CONSTRUCTOR.
            //  YOU CAN CHANGE THE CSV FOR THE EMPLOYEE DATABASE IN MAIN.
            //
            //                    ENJOY !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    //string stateInput = args[0];
                    //string earnedIncome = args[1];
                    //TaxCalculator.ComputeTaxFor(stateInput, earnedIncome);

            string CsvFile = "employees.csv";
            StreamReader EmployeeInformation = new StreamReader(CsvFile);
            List<EmployeeRecord> finishedList = CreateEmployeeList(EmployeeInformation);
            Console.WriteLine("\n********* Closing and Disposing of Stream for Employeee Info *************\n");
            EmployeeInformation.Close();
            EmployeeInformation.Dispose();
            TaxCalculator.CloseTaxInfo();
            Console.WriteLine("\n -------- Building Your Employee List --------- \n");
            bool sortTheList = true;
            while (sortTheList)
            {
                string howToSort = " ";
                Console.WriteLine("//////  How Would You Like Your List Sorted? //////\n");
                Console.WriteLine("To Sort By Name Enter >> Name.");
                Console.WriteLine("To Sort By State Enter >> State.");
                Console.WriteLine("To Sort By Employee ID Enter >> ID.");
                Console.WriteLine("To Sort By Yearly Pay Enter >> Pay.");
                Console.WriteLine("To Sort By Tax Due Enter >> Tax.");
                Console.WriteLine("The Default Sort Will Be By >> Name");
                Console.Write("Enter Your Choice Or Press Any Key For Default:>> ");
                howToSort = Console.ReadLine();
                List<EmployeeRecord> sortedList = sortList(finishedList, howToSort);
                printList(sortedList);
                Console.WriteLine("\nWould You Like To Sort Again?");
                Console.Write("Enter Y for yes or Just Enter Any Other Character To Exit:>> ");
                string sortAgain = Console.ReadLine().ToLower();
                Console.WriteLine();
                if (sortAgain != "y")
                {
                    Console.WriteLine("\n------- Thank You For Visiting My Employee Tax Calculation Project!!! ------- \n");
                    sortTheList = false;
                }
            }
        }
        static List<EmployeeRecord> sortList(List<EmployeeRecord> list, string sortBy)
        {//sort by state, Employee ID, Employee Name, yearly pay, tax due
            switch (sortBy.ToLower())
            {
                case "state":
                    Console.WriteLine("\n//// List Ordered By State. ////\n");
                    return list.OrderBy(x => x.StateCode).ToList();
                case "id":
                    Console.WriteLine("\n//// List Ordered By Employee ID. ////\n");
                    return list.OrderBy(x => x.Id).ToList();
                case "name":
                    Console.WriteLine("\n//// List Ordered By Employee Name. ////\n");
                    return list.OrderBy(x => x.Name).ToList();
                case "tax":
                    Console.WriteLine("\n//// List Ordered By Taxes Owed. ////\n");
                    return list.OrderBy(x => x.TaxesOwed).ToList();
                case "pay":
                    Console.WriteLine("\n//// List Ordered By Yearly Pay. ////\n");
                    return list.OrderBy(x => x.TotalEarned).ToList();
                default:
                    Console.WriteLine("\n//// Default List Ordered By Name. ////\n");
                    return list.OrderBy(x => x.Name).ToList();
            }
        }
        static void printList(List<EmployeeRecord> empoyee)
        {//EmployeeRecord(int Id, string name, string code, int hours, decimal rate)
            foreach (EmployeeRecord record in empoyee)
            {
                Console.WriteLine($"Emplyee Id is:{record.Id}. Employee Name is: {record.Name}.\n" +
                    $"Employee State Code is: {record.StateCode}. Employee Hours this year equal: {record.HoursWorked}.\n" +
                    $"Employee Hourly Rate is: {record.PayRate}. Yearly Pay is: {record.TotalEarned}.\n" +
                    $"The Taxes Owed by Employee are: {record.TaxesOwed}.\n");
            }
        }

        static string CsvFile = "employeeTest.csv";
        static List<EmployeeRecord> CreateEmployeeList(StreamReader employees)

        {
            List<EmployeeRecord> employeesList = new List<EmployeeRecord>();
            Console.WriteLine($"Loaded {CsvFile}\n");
            int employeeId;
            string employeeName = "";
            string employeeCode = "";
            int employeeHours;
            decimal employeeRate;
            bool isReading = true;
            while (isReading)
            {
                string employeeInfoString = employees.ReadLine();
                if (employeeInfoString != null && employeeInfoString != "")
                {
                    string[] employeeInfoArray = employeeInfoString.Split(",");

                    int isId;
                    bool idIsInt = int.TryParse(employeeInfoArray[0], out isId);
                    try
                    {
                        if (!idIsInt)
                        {
                            throw new Exception($" ////// ERROR !!!!! /////" +
                                $"Employee ID returned {employeeInfoArray[0]}.\n" +
                                $"This feild requires an integer. Employee skipped\n");
                        }
                        else
                        {
                            employeeId = isId;
                        }
                    }catch (Exception e)
                    {
                        Console.Error.WriteLine(e.Message);
                        continue;
                    }
                    int nameInt;
                    decimal nameDecimal;
                    bool isNameInt = int.TryParse(employeeInfoArray[1], out nameInt);
                    bool isNameDecimal = decimal.TryParse(employeeInfoArray[1], out nameDecimal);
                    try
                    {
                        if (isNameInt || isNameDecimal || employeeInfoArray[1] == "")
                        {
                            throw new Exception($"  ////// ERROR !!!! /////" +
                                $"Employee Name returned: {employeeInfoArray[1]}.\n" +
                                $"This field requires a string of letters. Employee skipped\n");
                        }
                        else
                        {
                            employeeName = employeeInfoArray[1];
                        }
                    }catch(Exception e)
                    {
                        Console.Error.WriteLine(e.Message);
                        continue;
                    }
                    int codeInt;
                    decimal codeDecimal;
                    bool isCodeInt = int.TryParse(employeeInfoArray[2], out codeInt);
                    bool isCodeDecimal = decimal.TryParse(employeeInfoArray[2], out codeDecimal);
                    string[] states = { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };
                    bool validState = states.Contains(employeeInfoArray[2].ToUpper());
                    try
                    {
                        if (!validState)
                        {
                            throw new Exception($"  /////// ERROR !!!!! /////" +
                                $"State Code is: {employeeInfoArray[2]}. State code must be a valid state.\n" +
                                $"Employee Skipped\n");

                        }
                        else if (isCodeInt || isCodeDecimal || employeeInfoArray[2] == "")
                        {
                            throw new Exception($" /////// ERROR !!!!! /////" +
                                $"Employee Code returned: {employeeInfoArray[2]}.\n" +
                                $"This field requires a string of letters. Employee skipped\n");
                        }
                        else
                        {
                            employeeCode = employeeInfoArray[2].ToUpper();
                        }
                    }catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                        continue;
                    }
                    int isHours;
                    bool hoursIsInt = int.TryParse(employeeInfoArray[3], out isHours);
                    try
                    {
                        if (!hoursIsInt)
                        {
                            throw new Exception($" //// ERROR !!!! ////" +
                                $"Employee Hours returned: {employeeInfoArray[3]}.\n" +
                                $"This feild requires an integer. Employee skipped\n");
                        }
                        else
                        {
                            employeeHours = isHours;
                        }
                    }catch(Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                        continue;
                    }
                    decimal isRate;
                    bool rateIsDecimal = decimal.TryParse(employeeInfoArray[4], out isRate);
                    try
                    {
                        if (!rateIsDecimal)
                        {
                            throw new Exception($" //// ERROR !!!! ////" +
                                $"Employee Rate returned: {employeeInfoArray[4]}.\n" +
                                $"This feild requires a Decimal or integer. Employee skipped\n");
                        }
                        else
                        {
                            employeeRate = isRate;
                        }
                    }catch(Exception e)
                    {
                        Console.Error.WriteLine(e.Message);
                        continue;
                    }

                    EmployeeRecord Employee = new EmployeeRecord(employeeId, employeeName, employeeCode, employeeHours, employeeRate);
                    employeesList.Add(Employee);
                }
                else
                {
                    isReading = false;
                }
            }
            Console.WriteLine("returning employee list: " + employeesList);
            return employeesList;
        }
    } 
    
    class TaxCalculator
    {
        static string CsvFile { get; set; }
        static StreamReader _taxInormation;
        static StreamReader TaxInformation { 
            get { return _taxInormation; } 
            set { _taxInormation = value; } 
        }
        static bool isSilent = true;

        static public void CloseTaxInfo()
        {
            Console.WriteLine("\n *********** Closing and Disposing of Stream for Tax Info *********** \n");
            _taxInormation.Close();
            _taxInormation.Dispose();
        }
        static TaxCalculator()
        {
            
                CsvFile = "taxtable.csv";
                TaxInformation = new StreamReader(CsvFile);
            Console.WriteLine($"Loaded {CsvFile}\n");
        }
        static public decimal ComputeTaxFor(string stateAbrev, string moneyEarned)
        {
            Console.WriteLine("******************** Starting Calculation *******************\n");

            Console.WriteLine("Welcome! Would you like a verbose statement or a silent statement?\n");
            Console.Write("Please Enter 'V' for Verbose or 'S' for Silent: ");
            bool haveVerboseInput = false;

            while (!haveVerboseInput)
            {
                string verboseOrSilent = Console.ReadLine().Trim();
                verboseOrSilent = verboseOrSilent.ToLower();
                if (verboseOrSilent == "v")
                {
                    isSilent = false;
                    Console.WriteLine($"\nYou chose Verbose.");
                    haveVerboseInput = true;
                }
                else if (verboseOrSilent == "s")
                {
                    isSilent = true;
                    Console.WriteLine($"\nYou chose Silent.");
                    haveVerboseInput = true;
                }
                else
                {
                    Console.Write("Invalid Choice>>Please Enter (V)erbose or (S)ilent: ");
                }

            }
            bool isReading = true;
            string state = stateAbrev.ToUpper();
            decimal incomeEarned = decimal.Parse(moneyEarned);
            decimal taxDue = 0M;
            decimal formerUpperAmount = 0;
            long accountedForAmount = 0;
            while (isReading)
            {
                string TaxInformationRecord = TaxInformation.ReadLine();
                if (TaxInformationRecord != null)
                {    
                    if (TaxInformationRecord.Contains(state.Trim()))
                    {
                        string[] TaxInformationRecordSplit = TaxInformationRecord.Split(",");
                        int nameTestInt;
                        bool isStateNameInt = int.TryParse(TaxInformationRecordSplit[1], out nameTestInt);
                        double nameTestDecimal;
                        bool isStateNameDecimal = double.TryParse(TaxInformationRecordSplit[1], out nameTestDecimal);
                        try{
                            if (TaxInformationRecordSplit[1] == "" || isStateNameDecimal || isStateNameInt)
                            {
                                throw new Exception($" //// ERROR !!!! //// " +
                                    $"There is an invalid state name listed for this line of data\n " +
                                    $"for the state of: {TaxInformationRecordSplit[0]}. Entry will not be included in calculation.\n");
                            }
                        }catch(Exception e)
                        {
                            Console.Error.WriteLine (e.Message);
                            continue;
                        }
                        int incomeLevelLower;
                        bool parsedIncomeFloor = int.TryParse(TaxInformationRecordSplit[2], out incomeLevelLower);
                        try
                        {
                            if (!parsedIncomeFloor)
                            {
                                throw new Exception($"  //// ERROR !!!! ////" +
                                    $"This entry for the state of {TaxInformationRecordSplit[0]} has an " +
                                    $"invalid entry for the income lower threshold.\n Entry will not be included in Calculation.\n");
                            }
                        }catch (Exception e)
                        {
                            Console.Error.WriteLine(e.Message);
                            continue;
                        }
                        long incomeLevelUpper;
                        bool parsedIncomeUpper = long.TryParse(TaxInformationRecordSplit[3], out incomeLevelUpper);
                        try
                        {
                            if (!parsedIncomeUpper)
                            {
                                throw new Exception($"  //// ERROR !!!! ////" +
                                    $"This entry for the state of {TaxInformationRecordSplit[0]} has an " +
                                    $"invalid entry for the income upper threshold.\n Entry will not be included in Calculation.\n");
                            }
                        }catch( Exception e)
                        {
                            Console.Error.WriteLine(e.Message);
                            continue;
                        }
                        decimal returnedTaxRate;
                        bool parsedReturnTaxRate = decimal.TryParse(TaxInformationRecordSplit[4], out returnedTaxRate);
                        try
                        {
                            if (!parsedReturnTaxRate)
                            {
                                throw new Exception($"   //// ERROR !!!! ////" +
                                    $"This entry for the state of {TaxInformationRecordSplit[0]} has an " +
                                    $"invalid entry for the tax rate.\n Entry will not be included in Calculation.\n");
                            }
                        }catch(Exception e)
                        {
                            Console.Error.WriteLine(e.Message);
                            continue;
                        }
                            if (incomeEarned >= incomeLevelUpper)
                        {
                            decimal instanceTaxWithheld = (incomeLevelUpper - formerUpperAmount) * returnedTaxRate;//amount of tax withheld on this iteration.
                            taxDue += instanceTaxWithheld;
                            accountedForAmount = incomeLevelUpper;
                            formerUpperAmount = incomeLevelUpper;
                            if (!isSilent)
                            {
                                Console.WriteLine($"{Decimal.Round(instanceTaxWithheld,2)} withheld for this instance of {incomeLevelUpper} or less earned.\n" +
                                $"The tax rate for this bracket is {returnedTaxRate}. Total tax witheld so far is: {Decimal.Round(taxDue),2}");
                                Console.WriteLine($"Amount left to be acounted for is {incomeEarned - accountedForAmount}");
                            }
                            
                        }  
                        if (incomeEarned >= incomeLevelLower && incomeEarned < incomeLevelUpper)
                        {
                            if (!isSilent)
                            {
                                Console.WriteLine($"Found a lower level of {incomeLevelLower} and upper of {incomeLevelUpper} your tax rate is {returnedTaxRate}.\n".Trim());
                            }
                            decimal instanceTaxWithheld = (incomeEarned - formerUpperAmount) * returnedTaxRate;//amount of tax withheld on this iteration.
                            taxDue += instanceTaxWithheld;
                            accountedForAmount += incomeLevelUpper;
                            if (!isSilent)
                            {
                                Console.WriteLine($"{instanceTaxWithheld} withheld for this instance of {incomeLevelUpper} or less earned. Amount of\n"+
                                    $"income acccounted for is up to {incomeEarned}. Total tax witheld is: {Decimal.Round(taxDue,2)}\n\n\n");
                                Console.WriteLine("////////////    Ending Current Calculation ////////////////////////////////\n\n");
                            }
                            _taxInormation.DiscardBufferedData();
                            _taxInormation.BaseStream.Position = 0;
                            return Decimal.Round(taxDue, 2);
                        }
                    }
                    //Console.WriteLine();
                }
                else
                {
                    isReading = false;
                }
            }
            _taxInormation.DiscardBufferedData();
            _taxInormation.BaseStream.Position = 0;
            return Decimal.Round(taxDue, 2);
        }
    }// -------------------------  Tax Calculator ----------------------------------------------------

}
