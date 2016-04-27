using System.Collections.Generic;
using System;

class Program
{
    static void Main()
    {
        
        string creditCardNumber = "6168 0155 1234 0624";
        Console.WriteLine("\nHello there,\n\nThe given card number: {0}",creditCardNumber);
        // task # 1
        Console.WriteLine("Task #1\nCard vendor: {0}",GetCreditCardVendor(creditCardNumber));
        // task # 2
        Console.WriteLine("Task #2\nCard number: {0}",IsCreditCardNumberValid(creditCardNumber)?"valid":"invalid");
        //task # 3
        Console.WriteLine("Task #3\nNext valid credit number according to Luhn formula: {0}",GenerateNextCreditCarddNumber(creditCardNumber));
        Console.WriteLine("\nThank you\n");
    }
    
    static string GenerateNextCreditCarddNumber(string cardNumber)
    {
        // check if card number contains white spaces
        if (cardNumber.Contains(" "))
            cardNumber = cardNumber.Replace(" ", "");
        // cast string to decimal and increase by 1 
        decimal nextValidCardNumber = Decimal.Parse(cardNumber)+1;
        nextValidCardNumber++;

        // check if new card number pass verification (Luhn algo) 
        while (!IsCreditCardNumberValid(nextValidCardNumber.ToString()))
                nextValidCardNumber++;

        return nextValidCardNumber.ToString(); 
    }
    
    static bool IsCreditCardNumberValid(string cardNumber)
    {
        int sumEven=0, sumOdd=0;
        
        // check if card number contains white spaces
        if (cardNumber.Contains(" "))
            cardNumber = cardNumber.Replace(" ", "");
            
            for(int i = cardNumber.Length-1; i>=0; i--)
            {
                int curDigit = Int32.Parse(cardNumber.Substring(i,1));
                
                if (i%2==0)
                {
                    // Two times each even digit ()
                    // if multiplication > 9 substrac 9
                    // sum the digits of each multiplication:
                    sumEven += curDigit*2>9 ? (curDigit*2-9) : (curDigit*2);
                }
                else
                {
                    // sum of the odd digits
                    sumOdd+=curDigit;
                }
            }
            
        // check if modulo 10 is equal to 0 
        return (sumEven+sumOdd)%10 == 0;
    }

    static string GetCreditCardVendor(string cardNumber)
    {
        // check if card number contains white spaces
        if (cardNumber.Contains(" "))
            cardNumber = cardNumber.Replace(" ", "");
        // create lookup dictionary with issuing network's name and IIN
        // IIN contains the numbers and the ranges splited by ','
        Dictionary<string,string> cardVendorList = new Dictionary<string,string> {{"34,37","American Express"},{"51-55","MasterCard"},{"50,56-69","Maestro"},{"3528-3589","JCB"},{"4","Visa"}};
        // go through each key value in dictionary
        // check if given IIN can be recognizable  
        foreach(KeyValuePair<string,string> currentCardVendor in cardVendorList)
        {
            foreach(string currMask in currentCardVendor.Key.ToString().Split(','))
            {
                if (currMask.Contains("-") == true)
                {
                    // in case of range
                    string[] rangeCheck = currMask.Split('-');
                    if ( Int32.Parse(cardNumber.Substring(0,rangeCheck[0].Length)) >= Int32.Parse(rangeCheck[0]) && Int32.Parse(cardNumber.Substring(0,rangeCheck[0].Length)) <= Int32.Parse(rangeCheck[1]) )
                    {
                        return  currentCardVendor.Value;
                    }
                }
                else
                {
                    //in case of single digit
                    if (Int32.Parse(currMask) == Int32.Parse(cardNumber.Substring(0,currMask.Length)))
                    {
                        return  currentCardVendor.Value;
                    }
                }
            }
        }
        // in case if the given IIN can not be recognized
        return "Unknow";
    }
}
