using System.Collections.Generic;
using System;

class Program
{
    static void Main()
    {

        string creditCardNumber = "6168 0155 1234 0624";
        //creditCardNumber = "3434343434343433";
        Console.WriteLine("\nHello there,\n\nThe given card number: {0}", creditCardNumber);
        // task # 1
        Console.WriteLine("Task #1\nCard vendor: {0}", GetCreditCardVendor(creditCardNumber));
        // task # 2
        Console.WriteLine("Task #2\nCard number: {0}", IsCreditCardNumberValid(creditCardNumber) ? "valid" : "invalid");
        //task # 3
        Console.WriteLine("Task #3\nNext valid credit card number: {0}", GenerateNextCreditCarddNumber(creditCardNumber));
        Console.WriteLine("\nThank you\n");
    }

    // check if value belong to the range (incldue range limits) or eqs to the number
    // currValue <string> is single number (ex. "13")
    // range <string> is a separaited by comma set of single values or ranges (ex. "2,12-15,30-40")
    static bool CheckRange(string currValue, string range)
    {
        //split range
        string[] subRange = range.Split(',');
        foreach (string currSubRange in subRange)
        {
            if (currSubRange.Contains("-") == true)
            {
                // in case of range
                string[] rangeCheck = currSubRange.Split('-');
                if (Int32.Parse(currValue.Substring(0, rangeCheck[0].Length)) >= Int32.Parse(rangeCheck[0]) && Int32.Parse(currValue.Substring(0, rangeCheck[0].Length)) <= Int32.Parse(rangeCheck[1]))
                {
                    return true;
                }
            }
            else
            {
                //in case of single digit
                if (Int32.Parse(currSubRange) == Int32.Parse(currValue.Substring(0, currSubRange.Length)))
                {
                    return true;
                }
            }
        }
        return false;
    }

     static string GenerateNextCreditCarddNumber(string cardNumber)
    {
        // check if card number contains white spaces
        if (cardNumber.Contains(" "))
            cardNumber = cardNumber.Replace(" ", "");
        // cast string to decimal and increase by 1 
        decimal nextValidCardNumber = Decimal.Parse(cardNumber) + 1;

        // check if new card number pass verification (Luhn algo) 
        while (!IsCreditCardNumberValid(nextValidCardNumber.ToString()))
            nextValidCardNumber++;

        if (String.Compare ( GetCreditCardVendor(nextValidCardNumber.ToString()), GetCreditCardVendor(cardNumber)) == 0)
        {
            return nextValidCardNumber.ToString();
        }
        else
        {
            return "No valid card number for given vendor";
        }
    }

    static bool IsCreditCardNumberValid(string cardNumber)
    {
        int sumOfDigits = 0;

        // check if card number contains white spaces
        if (cardNumber.Contains(" "))
            cardNumber = cardNumber.Replace(" ", "");

        for (int i = cardNumber.Length - 1, j = 1; i >= 0; i--, j++)
        {
            int curDigit = Int32.Parse(cardNumber.Substring(i, 1));

            if (j % 2 == 0)
            {
                // Two times each even digit ()
                // if multiplication > 9 substrac 9
                // sum the digits of each multiplication:
                sumOfDigits += curDigit * 2 > 9 ? (curDigit * 2 - 9) : (curDigit * 2);
            }
            else
            {
                // sum of the odd digits
                sumOfDigits += curDigit;
            }
        }

        // check if modulo 10 is equal to 0 
        return sumOfDigits % 10 == 0;
    }

    static string GetCreditCardVendor(string cardNumber)
    {
        // check if card number contains white spaces
        if (cardNumber.Contains(" "))
            cardNumber = cardNumber.Replace(" ", "");

        // create lookup List with issuing network's name and IIN, and number lenght
        // IIN/number lenght contains the numbers and the ranges splited by ','
        List<CardInfo> cardVendorList = new List<CardInfo>();
        cardVendorList.Add(new CardInfo("American Express", "34,37", "15"));
        cardVendorList.Add(new CardInfo("MasterCard", "51-55", "16"));
        cardVendorList.Add(new CardInfo("Maestro", "50,56-69", "12-19"));
        cardVendorList.Add(new CardInfo("JCB", "3528-3589", "16"));
        cardVendorList.Add(new CardInfo("Visa", "4", "13,16,19"));

        // check given card number
        foreach(CardInfo currCard in cardVendorList)
        {
            if (CheckRange(cardNumber, currCard.IIN) && CheckRange(cardNumber.Length.ToString(), currCard.cardNumberLenght)) 
            {
                return currCard.cardIssuer;
            }
        }

        // in case if the given IIN can not be recognized
        return "Unknown";
    }
}

public class CardInfo
{
    public string cardIssuer { get; set; }
    public string IIN { get; set; }
    public string cardNumberLenght { get; set; }

    public CardInfo(string cardIssuer, string IIN, string cardNumberLenght)
    {
        this.cardIssuer = cardIssuer;
        this.IIN = IIN;
        this.cardNumberLenght = cardNumberLenght;
    }

    public CardInfo(CardInfo ob)
    {
        this.cardIssuer = ob.cardIssuer;
        this.IIN = ob.IIN;
        this.cardNumberLenght = ob.cardNumberLenght;
    }
}
