using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using JFGCDatabase;

using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace invgun_collection
{
    public partial class Form1 : Form
    {

        FileStream ostrm;
        StreamWriter writer;

        Dictionary<string, myCount> itemCount = new Dictionary<string, myCount>();
        
        

        Dictionary<string, decimal> catogoryCost = new Dictionary<string, decimal>
        {
            {"1", 0 },
             // not printed out on report {"1N", 0 },
            {"2", 0 },
            {"3", 0 },
            {"4", 0 },
            {"5", 0 },
            {"6", 0 },
            {"7", 0 },
            {"10", 0 },
            {"11", 0 },
            {"12", 0 },
            {"14", 0 },
            {"15", 0 },
            {"16", 0 },
            {"17", 0 },
            {"20", 0 },
            {"21", 0 },
            {"23", 0 },
            {"24", 0 },
            {"25", 0 },
            {"26", 0 },
            {"27", 0 },
            {"28", 0 },
            {"29", 0 },
             // not printed out on report {"30", 0 },
            {"31", 0 },
             // not printed out on report {"32", 0 },
            {"33", 0 },
            {"34", 0 },
            {"35", 0 },
            {"36", 0 },
            {"37", 0 },
             // not printed out on report {"38", 0 },
            {"39", 0 },
            {"40", 0 },
            {"45", 0 },
            {"46", 0 },
             // not printed out on report {"47", 0 },
            {"49", 0 },
            {"199",0 }
             // not printed out on report {"50", 0 },
             // not printed out on report {"54", 0 },
             // not printed out on report {"98", 0 },
             // not printed out on report {"99", 0 },
             // not printed out on report {"100", 0 },
             // not printed out on report {"101", 0 },
             // not printed out on report {"102", 0 },
             // not printed out on report {"103", 0 }
        };


       


        Thread thread;
        string folderName = "";

        Dictionary<string, string> deptName = new Dictionary<string, string>
        {
            {"1", "Cut Flowers" },
            {"1N", "DEPT 1 NON CUTS" },
            {"2", "Ribbons & Bows" },
            {"3", "Design - Cut" },
            {"4", "Holiday Silk" },
            {"5", "Dried & Silk" },
            {"6", "Candles" },
            {"7", "Balloons" },
            {"10", "Green Houseplants" },
            {"11", "Bloom Houseplants" },
            {"12", "Potted Bulbs - Hp" },
            {"14", "Pottery-Inside/Outside" },
            {"15", "Bedding Plants" },
            {"16", "Perennials" },
            {"17", "Vegetables" },
            {"20", "Woodies" },
            {"21", "Holiday Live Trees" },
            {"23", "Bulbs & Tubers" },
            {"24", "Seeds - Flwr/Veg" },
            {"25", "Garden Supplies" },
            {"26", "Bagged Goods - Gs" },
            {"27", "Ponds & Supplies" },
            {"28", "Bulk Ground Covers" },
            {"29", "Patio Living" },
            {"30", "Contributions" },
            {"31", "Concrete - All" },
            {"32", "Bagged Goods - LS" },
            {"33", "Fashion" },
            {"34", "Propane" },
            {"35", "Nature/Gift" },
            {"36", "Cards & Gift Wrap" },
            {"37", "Seasonal Gift" },
            {"38", "Gift - Corp" },
            {"39", "Cookies/Candy/Food" },
            {"40", "Jewelry" },
            {"45", "Christmas Ornaments" },
            {"46", "Christmas Greens & Wreaths" },
            {"47", "Cut Christmas Trees" },
            {"49", "Pumpkins" },
            {"50", "Teleflora Out" },
            {"54", "Misc Dump/Cut" },
            {"98", "Montgomery Bag Tax" },
            {"99", "Plant Service Fee" },
            {"100", "Delivery Fees" },
            {"101", "MISC CHAGES TEL/FTD" },
            {"102", "comments" },
            {"103", "MISC BANK FEES XXXX-07"},
            {"199", "MISC" }

        };


        Dictionary<string, decimal> deptMargin = new Dictionary<string, decimal>
        {
            {"1", 65.20m},
            {"1N", 65.20m},
            {"2", 59.00m},
            {"3", 59.00m},
            {"4", 64.20m},
            {"5", 64.70m},
            {"6", 64.20m},
            {"7", 64.70m},
            {"10", 69.50m},
            {"11", 69.50m},
            {"12", 69.50m},
            {"14", 60.32m},
            {"15", 66.00m},
            {"16", 69.27m},
            {"17", 66.00m},
            {"20", 66.00m},
            {"21", 66.00m},
            {"23", 64.20m},
            {"24", 56.20m},
            {"25", 59.50m},
            {"26", 56.20m},
            {"27", 56.20m},
            {"28", 58.00m},
            {"29", 64.00m},
            {"31", 61.00m},
            {"32", 64.20m},
            {"33", 64.00m},
            {"34", 64.49m},
            {"35", 62.50m},
            {"36", 63.00m},
            {"37", 63.00m},
            {"39", 64.00m},
            {"40", 64.00m},
            {"45", 63.00m},
            {"46", 61.00m},
            {"47", 62.20m},
            {"49", 66.00m}
        };


        

       

        public Form1()
        {
            InitializeComponent();

            string test = CheckDigit("02327193643");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string itemCat = "15";

            decimal itemPrice = 24.99m;

            decimal itemCost = itemPrice - ((deptMargin[itemCat] / 100) * itemPrice);
            string line = "037321001416        SULFUR FUNGICIDE #1 LB             8.99  29.0001/01/208019:16:06";
            string itemNo = line.Substring(0, 20);
            string itemDesc = line.Substring(20, 32);
            int itemQty = Int32.Parse(line.Substring(59, 4));

            thread = new Thread(workerThread);
            thread.Start();
        }

        public void ChooseFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                folderName = textBox1.Text;
            }
        }

        public void workerThread()
        {
           
            var fileReport = System.Windows.Forms.Application.StartupPath + "/INVCOST.csv";
            var fileReportCount = System.Windows.Forms.Application.StartupPath + "/INVCOUNT.csv";
            string itemNo = "";
            string itemCat = "1";
            string lastItemCat = "1";
            string line = "";
            string itemDesc = "";
            string tmp = "";
            int itemQty = 0;
            decimal itemPrice = 0;
            decimal itemCost = 0;
            decimal itemTotalCost = 0;

            bool ItemMissing = false;
            string prevItemDesc="";
            string nextItemDesc = "";

           


            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream("./log.txt", FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }


            Console.SetOut(writer);
            Console.WriteLine("Starting Collection of guns.....");
            using (FileStream fsINV = File.Open(fileReport, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (FileStream fsINVCount = File.Open(fileReportCount, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (StreamWriter swINV = new StreamWriter(fsINV))
            using (StreamWriter swINVCount = new StreamWriter(fsINVCount))
            using (var jfgc = new JFGCEntities())
            {
                Console.WriteLine("Connected to Johnsons Database.....");

                writer.Flush();


                string[] fileEntries = Directory.GetFiles(folderName);

                Console.WriteLine("Files collected.... " + fileEntries.Length.ToString());
                writer.Flush();

                foreach (string fileName in fileEntries)
                {
                    Console.WriteLine("Processing File.... " + fileName);
                    writer.Flush();

                    using (FileStream fsGunFile = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader srGunFile = new StreamReader(fsGunFile))
                    {
                        Console.WriteLine("Opened File.... " + fileName);
                        writer.Flush();

                        while ((line = srGunFile.ReadLine()) != null)
                        {
                            Console.WriteLine("Reading File.... " + line);
                            writer.Flush();

                            //string [] array = line.Split(' ').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();

                            itemNo = line.Substring(0, 20);
                            itemNo = itemNo.Trim();
                            itemDesc = line.Substring(20, 32);
                            itemDesc = itemDesc.Trim();
                            itemQty = 4;
                            itemPrice = 0;

                            // get price from gun
                            try
                            {
                                tmp = line.Substring(53, 6);
                                itemPrice = Decimal.Parse(tmp);

                                //if (itemPrice == 0) itemQty = 8;

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error. quantity invalid '" + tmp + "' " + ex);
                                itemPrice = 0;
                            }

                            // get quantity from gun
                            try
                            {
                                tmp = line.Substring(59, 4);
                                itemQty = Int32.Parse(tmp);

                                if (itemQty == 0) itemQty = 1;

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error. quantity invalid '" + tmp + "' " + ex);
                                itemQty = 1;
                            }



                            Console.WriteLine("Parse Line 1.... {0} {1} {2}", itemNo, itemDesc, itemQty);
                            writer.Flush();

                            if (itemNo.Length > 0 || itemDesc.Length > 0)
                            {



                                string newItem = "";

                                var itemRec = GetItem(itemNo, itemDesc, jfgc, out newItem);

                                if (itemRec != null)
                                {
                                    if (ItemMissing)
                                    {
                                        ItemMissing = false;
                                        Console.WriteLine("*** Error: Item not found ...,,," + itemDesc);
                                        writer.Flush();
                                    }

                                    itemCat = itemRec.CATEG_COD.TrimStart('0');

                                    Console.WriteLine("Parse Line 2.... {0}, {1}, {2}, Dept: {3}", itemNo, itemDesc, itemQty, itemCat);
                                    writer.Flush();



                                    Console.WriteLine("Item found in Database .... " + itemRec.DESCR + "  " + itemCat);
                                    writer.Flush();

                                    itemCost = (Decimal)itemRec.LST_COST * itemQty;

                                    Console.WriteLine("Calculate Cost .... " + itemRec.LST_COST.ToString() + "," + itemQty.ToString() + "," + itemCost);
                                    writer.Flush();

                                    if (itemCost > 350)
                                        Console.WriteLine("*** Invalid entry Cost .... ," + itemNo + " ," + itemDesc + "," + itemRec.ITEM_NO + "," + itemRec.DESCR + ", " + itemQty.ToString() + ", " + itemRec.LST_COST + "," + itemRec.PRC_1.ToString() + "," + itemCat + "," + fileName);


                                    if (itemCat == "28")
                                    {
                                        writer.Flush();
                                    }
                                    catogoryCost[itemCat] = Decimal.Add(catogoryCost[itemCat], itemCost);

                                    if (itemCount.ContainsKey(itemRec.ITEM_NO))
                                    {
                                        var mc = new myCount();

                                        int oldCnt = itemCount[itemRec.ITEM_NO].count;

                                        mc.count = oldCnt + itemQty;
                                        mc.dept = itemCat;
                                        itemCount[itemRec.ITEM_NO] = mc;
                                    }
                                    else
                                    {
                                        var mc = new myCount();

                                        mc.count = itemQty;
                                        mc.dept = itemCat;
                                        itemCount[itemRec.ITEM_NO] = mc;
                                    }
                                    

                                    //catogoryQuantity[itemCat].Add(itemQty);
                                    //catogoryPrices[itemCat].Add((Decimal)itemRec.LST_COST);

                                    lastItemCat = itemCat;




                                }
                                else
                                {

                                    // try to get and estimated cost based on retail price stored from the gun and calulated based on last dept margin
                                    if (ItemMissing)
                                    {
                                        Console.WriteLine("*** Error: Item not found ...,,," + itemDesc);
                                        writer.Flush();
                                    }

                                    ItemMissing = true;

                                    itemCat = lastItemCat;
                                    itemCost = itemPrice - ((deptMargin[itemCat] / 100) * itemPrice);


                                    Console.WriteLine("*** Error: Item not found ....," + itemNo + ", " + newItem + "," + itemDesc + ", " + itemQty.ToString() + "," + itemPrice.ToString() + "," + itemCost + "," + itemCat + "," + fileName + "," + prevItemDesc);
                                    writer.Flush();

                                    if (this.checkBox1.Checked)
                                    {


                                        Console.WriteLine("Parse Line ... " + itemNo + "   " + itemQty.ToString());
                                        writer.Flush();


                                        itemTotalCost = itemCost * itemQty;

                                        Console.WriteLine("Calculate Cost .... " + itemCost.ToString() + "," + itemQty.ToString() + "," + itemTotalCost + "," + itemTotalCost);
                                        writer.Flush();

                                        //if (itemCost > 350)
                                        //    Console.WriteLine("*** Invalid entry Cost .... ," + itemNo + " ," + itemDesc + ", " + itemQty.ToString() + "," + itemCost + "," + itemCat + "," + fileName + "," + itemPrice.ToString());



                                        catogoryCost["199"] = Decimal.Add(catogoryCost["199"], itemCost);

                                    }

                                }
                                writer.Flush();
                                prevItemDesc = itemDesc;

                            }
                            else
                            {

                            }

                        }

                        srGunFile.Close();
                    }

                }

                Console.WriteLine("Print out report ... ");

                foreach (KeyValuePair<string, decimal> entry in catogoryCost)
                {
                    // do something with entry.Value or entry.Key
                    line = String.Format("{0},{1},{2}", entry.Key, deptName[entry.Key], entry.Value);
                    swINV.WriteLine(line);
                }

                foreach (KeyValuePair<string, myCount> entry in itemCount)
                {
                    // do something with entry.Value or entry.Key
                    line = String.Format("{0},{1},{2}", entry.Key, entry.Value.count, entry.Value.dept);
                    swINVCount.WriteLine(line);
                }

                swINVCount.Flush();

                swINVCount.Close();

                swINV.Flush();

                swINV.Close();

                writer.Close();



                return;



            }

           

        }

        private IM_ITEM GetItem(string itemNo, string desc, JFGCEntities jfgc, out string newItem)
        {

           

            var itemRec = jfgc.IM_ITEM.FirstOrDefault();

            try
            {

                if (itemNo.Length > 0)
                {
                    itemRec = LookupBarCode(itemNo, jfgc);
                    if (itemRec == null)
                    {
                        itemNo = CheckDigit(itemNo);
                        if (itemNo != "")
                        {

                            itemRec = LookupBarCode(itemNo, jfgc);

                        }
                        if (itemRec == null)
                        {
                            if (desc.Length > 0)
                                itemRec = LookupBarCodeDesc(desc, jfgc);
                        }

                    }

                }
                else
                {
                    if (desc.Length > 0)
                        itemRec = LookupBarCodeDesc(desc, jfgc);
                }

                

            }catch(Exception ex)
            {
                Console.WriteLine("Exception Error: GetItem - " + ex);
                writer.Flush();
                itemRec = null;
            }

            newItem = itemNo;
            return itemRec;

        }

        private IM_ITEM LookupBarCode(string itemNo, JFGCEntities jfgc)
        {
            var itemRec = jfgc.IM_ITEM.FirstOrDefault();

            try
            {
                itemRec = (from item in jfgc.IM_ITEM
                               where item.ITEM_NO == itemNo
                               select item).FirstOrDefault();

                if (itemRec == null)
                {
                    Console.WriteLine("Not found in item table ");
                    // if not found in the item table lookup in the barcode table
                    // this is kind of the stupidist thing ever and sounds like this is sort of a broken architecture
                    // but whatever
                    //
                    itemRec = (from item in jfgc.IM_ITEM
                               join bc in jfgc.IM_BARCOD on item.ITEM_NO equals bc.ITEM_NO
                               where bc.BARCOD == itemNo
                               select item).FirstOrDefault();
                    if (itemRec == null)
                    {
                        Console.WriteLine("Not found in barcode table ");


                        for (int i = 0; i <= 3 && itemRec == null; ++i)
                        {
                            if (i==3)
                            {
                                itemNo = itemNo.Substring(1, itemNo.Length - 2);
                            }
                            else
                            {
                                itemNo = itemNo.Substring(0, itemNo.Length - i);
                            }
                           


                            try
                            {
                                itemRec = (from item in jfgc.IM_ITEM
                                           where item.ITEM_NO.Contains(itemNo)
                                           select item).SingleOrDefault();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception item table Contains " + ex);
                                break;
                            }


                            if (itemRec == null)
                            {
                                Console.WriteLine("Not found in item table Contains ");
                                // if not found in the item table lookup in the barcode table
                                // this is kind of the stupidist thing ever and sounds like this is sort of a broken architecture
                                // but whatever
                                //
                                try
                                {
                                    itemRec = (from item in jfgc.IM_ITEM
                                               join bc in jfgc.IM_BARCOD on item.ITEM_NO equals bc.ITEM_NO
                                               where bc.BARCOD.Contains(itemNo)
                                               select item).SingleOrDefault();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Exception barcode table Contains " + ex);
                                    break;
                                }


                            }


                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Error: LookupBarCode - " + ex);
                writer.Flush();
            }
            return itemRec;

        }

        private IM_ITEM LookupBarCodeDesc(string description, JFGCEntities jfgc)
        {
            var itemRec = jfgc.IM_ITEM.FirstOrDefault();

            try
            {
                itemRec = (from item in jfgc.IM_ITEM
                           where item.DESCR_UPR.Contains(description.ToUpper())
                           select item).SingleOrDefault();

                if (itemRec == null)
                {

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception item table Contains " + ex);
            }

            return itemRec;
        }


        private IM_ITEM LookupBarCodePrice(decimal price, JFGCEntities jfgc)
        {
            var itemRec = jfgc.IM_ITEM.FirstOrDefault();

            try
            {
                itemRec = (from item in jfgc.IM_ITEM
                           where item.PRC_1.Equals(price.ToString())
                           select item).SingleOrDefault();

                if (itemRec == null)
                {

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception item table Contains " + ex);
            }

            return itemRec;
        }

        private string CheckDigit(string data)
        {
            int digit = -1;
            string returnString = "";

            try
            {


                returnString =  data + _checksum_ean13(data).ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Error: CheckDigit - " + ex);
                writer.Flush();
            }

            return returnString;

        }

        private static int _checksum_ean8(String data)
        {
            // Test string for correct length
            if (data.Length != 7 && data.Length != 8)
                return -1;

            // Test string for being numeric
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] < 0x30 || data[i] > 0x39)
                    return -1;
            }

            int sum = 0;

            for (int i = 6; i >= 0; i--)
            {
                int digit = data[i] - 0x30;
                if ((i & 0x01) == 1)
                    sum += digit;
                else
                    sum += digit * 3;
            }
            int mod = sum % 10;
            return mod == 0 ? 0 : 10 - mod;
        }

        static int _checksum_ean13(String data)
        {

            bool pos = false;

            // Test string for being numeric
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] < 0x30 || data[i] > 0x39)
                    return -1;
            }

            int sum = 0;

            for (int i = data.Length-1; i >= 0; i--)
            {
                int digit = data[i] - 0x30;
                if (pos)
                {
                    sum += digit;
                    pos = false;
                }
                else
                {
                    sum += digit * 3;
                    pos = true;
                }
            }
            int mod = sum % 10;
            return mod == 0 ? 0 : 10 - mod;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            ChooseFolder();
        }
    }

    public struct myCount
    {
        public int count;
        public string dept;
    }

    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string Clean(this string value)
        {
            string s = Regex.Replace(value, @"[^\u0000-\u007F]+", string.Empty);
            s = s.Replace("-", String.Empty);
            s = s.Replace("/", String.Empty);
            return s;
        }
    }
}
