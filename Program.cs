using System;
using System.Collections.Generic;

namespace simpleCPU2
{
    public class Program
    {
        //the real secret to programming is to never give up

        static bool AndGate(bool in1, bool in2) //returns true only if both args are true
        {
            return (in1 && in2); //ideally the only c# native operator in the whole program
        }

        static bool NotGate(bool in1) //returns opposite of arg
        {
            return !(in1);
        }

        static bool NandGate(bool in1, bool in2) //returns false if both args are true, returns true otherwise 
        {
            return NotGate(AndGate(in1, in2));
        }

        static bool OrGate(bool in1, bool in2) //returns true if either arg is true
        {
            return NandGate(NotGate(in1), NotGate(in2));
        }

        static bool Norgate(bool in1, bool in2)
        {
            return NotGate(OrGate(in1, in2));
        }


        static bool XorGate(bool in1, bool in2) //returns true if either arg is true but not if both true or both false
        {
            return AndGate(OrGate(in1, in2), NandGate(in1, in2));

        }

        //2 half adders will allow us to add in binary
        //outs tuple to integrate into full adder

        static (bool sum, bool carry) HalfAdder(bool in1, bool in2) //TODO refactor to kill this emulation
        {
            if (AndGate(XorGate(in1, in2), NotGate(AndGate(in1, in2))))
            {
                return (true, false);
            }
            if (AndGate(NotGate(XorGate(in1, in2)), AndGate(in1, in2)))
            {
                return (false, true);
            }
            if (AndGate(XorGate(in1, in2), AndGate(in1, in2)))
            {
                return (true, true);
            }
            return (false, false);
        }

        //full adder 
        //accurate to https://imgur.com/a/WyCNcHN
        //diagram upper output is sum, lower output is carry

        static (bool sum, bool carry) FullAdder(bool in1, bool in2, bool _carry)
        {
            return (HalfAdder(HalfAdder(in1, in2).sum, _carry).sum,
                OrGate(HalfAdder(in1, in2).carry, HalfAdder(HalfAdder(in1, in2).sum, _carry).carry));
        }

        //adding assembly
        //8 full adders
        //each will process 1 vector of both arrays
        //i.e FullAdder[0] will do addend1[0] + addend2[0]

        static bool[] Add2Bytes(bool[] add1, bool[] add2)
        {
            bool[] result = new bool[8];
            bool[] carry = new bool[8]; //carry used as an array to conform to irl CPU adding

            result[0] = FullAdder(add1[0], add2[0], false).sum;
            carry[0] = FullAdder(add1[0], add2[0], false).carry;
            result[1] = FullAdder(add1[1], add2[1], carry[0]).sum;
            carry[1] = FullAdder(add1[1], add2[1], carry[0]).carry;
            result[2] = FullAdder(add1[2], add2[2], carry[1]).sum;
            carry[2] = FullAdder(add1[2], add2[2], carry[1]).carry;
            result[3] = FullAdder(add1[3], add2[3], carry[2]).sum;
            carry[3] = FullAdder(add1[3], add2[3], carry[2]).carry;
            result[4] = FullAdder(add1[4], add2[4], carry[3]).sum;
            carry[4] = FullAdder(add1[4], add2[4], carry[3]).carry;
            result[5] = FullAdder(add1[5], add2[5], carry[4]).sum;
            carry[5] = FullAdder(add1[5], add2[5], carry[4]).carry;
            result[6] = FullAdder(add1[6], add2[6], carry[5]).sum;
            carry[6] = FullAdder(add1[6], add2[6], carry[5]).carry;
            result[7] = FullAdder(add1[7], add2[7], carry[6]).sum;
            carry[7] = FullAdder(add1[7], add2[7], carry[6]).carry;

            //if (carry[7])
            //{
            //    throw new OverflowException();
            //}
            //TODO: implement exceptions and handlers - Add2Bytes currently just loses overflow data

            return result;

        }

        //adding more than 2 bytes
        //8 maximum? 
        //add 2, store result, then add 3rd, etc

        static bool[] AddBytes(bool[] in1, bool[] in2) //1st time overloading a function! add up to 8 bytes
        {
            return Add2Bytes(in1, in2);
        }

        static bool[] AddBytes(bool[] in1, bool[] in2, bool[] in3)
        {
            bool[] temp = new bool[8];
            temp = Add2Bytes(in1, in2);
            temp = Add2Bytes(temp, in3);
            return temp;
        }

        static bool[] AddBytes(bool[] in1, bool[] in2, bool[] in3, bool[] in4)
        {
            bool[] temp = new bool[8];
            temp = Add2Bytes(in1, in2);
            temp = Add2Bytes(temp, in3);
            temp = Add2Bytes(temp, in4);
            return temp;
        }

        static bool[] AddBytes(bool[] in1, bool[] in2, bool[] in3, bool[] in4, bool[] in5)
        {
            bool[] temp = new bool[8];
            temp = Add2Bytes(in1, in2);
            temp = Add2Bytes(temp, in3);
            temp = Add2Bytes(temp, in4);
            temp = Add2Bytes(temp, in5);
            return temp;
        }

        static bool[] AddBytes(bool[] in1, bool[] in2, bool[] in3, bool[] in4, bool[] in5, bool[] in6)
        {
            bool[] temp = new bool[8];
            temp = Add2Bytes(in1, in2);
            temp = Add2Bytes(temp, in3);
            temp = Add2Bytes(temp, in4);
            temp = Add2Bytes(temp, in5);
            temp = Add2Bytes(temp, in6);
            return temp;
        }

        static bool[] AddBytes(bool[] in1, bool[] in2, bool[] in3, bool[] in4, bool[] in5, bool[] in6, bool[] in7)
        {
            bool[] temp = new bool[8];
            temp = Add2Bytes(in1, in2);
            temp = Add2Bytes(temp, in3);
            temp = Add2Bytes(temp, in4);
            temp = Add2Bytes(temp, in5);
            temp = Add2Bytes(temp, in6);
            temp = Add2Bytes(temp, in7);
            return temp;
        }

        static bool[] AddBytes(bool[] in1, bool[] in2, bool[] in3, bool[] in4, bool[] in5, bool[] in6, bool[] in7, bool[] in8)
        {
            bool[] temp = new bool[8];
            temp = Add2Bytes(in1, in2);
            temp = Add2Bytes(temp, in3);
            temp = Add2Bytes(temp, in4);
            temp = Add2Bytes(temp, in5);
            temp = Add2Bytes(temp, in6);
            temp = Add2Bytes(temp, in7);
            temp = Add2Bytes(temp, in8);
            return temp;
        }

        static bool[] MakeNeg(bool[] arr) //returns 2's complimemt negative, does not turn negative integers positive
        {
            bool[] negOut = new bool[8];

            for (int i = 0; i < arr.Length - 1; i++)
            {
                negOut[i] = NotGate(arr[i]);
            }

            bool[] addOne = new bool[8];
            addOne[0] = true;
            addOne[1] = false;
            addOne[2] = false;
            addOne[3] = false;
            addOne[4] = false;
            addOne[5] = false;
            addOne[6] = false;
            addOne[7] = true; //sign bit, always true for neg. integers

            negOut = Add2Bytes(negOut, addOne);

            return negOut;
        }

        public static bool[] LeftShift(bool[] inbyte, int count) //necessary for multiplication, eventually emulate barrel shift
        {
            bool[] result = new bool[8]; //declare new array to simplify overwriting

            for (int j = 0; j < inbyte.Length - 1; j++)
            {
                if ((j + count) <= inbyte.Length - 1)
                {
                    result[j + count] = inbyte[j];
                }
            }
            return result;
        }

        //static bool[] Multiply(bool[] in1, bool[] in2) //not working yet ?
        //{
        //    bool[] temp1 = new bool[8];
        //    Array.Fill(temp1, false);
        //    bool[] temp2 = new bool[8];
        //    Array.Fill(temp2, false);
        //    bool[] temp3 = new bool[8];
        //    Array.Fill(temp3, false);
        //    bool[] temp4 = new bool[8];
        //    Array.Fill(temp4, false);
        //    bool[] temp5 = new bool[8];
        //    Array.Fill(temp5, false);
        //    bool[] temp6 = new bool[8];
        //    Array.Fill(temp6, false);
        //    bool[] temp7 = new bool[8];
        //    Array.Fill(temp7, false);
        //    bool[] temp8 = new bool[8];
        //    Array.Fill(temp8, false);


        //    temp1[0] = AndGate(in1[0], in2[0]);
        //    temp1[1] = AndGate(in1[1], in2[0]);
        //    temp1[2] = AndGate(in1[2], in2[0]);
        //    temp1[3] = AndGate(in1[3], in2[0]);
        //    temp1[4] = AndGate(in1[4], in2[0]);
        //    temp1[5] = AndGate(in1[5], in2[0]);
        //    temp1[6] = AndGate(in1[6], in2[0]);
        //    temp1[7] = AndGate(in1[7], in2[0]);

        //    temp2[0] = AndGate(in1[0], in2[1]);
        //    temp2[1] = AndGate(in1[1], in2[1]);
        //    temp2[2] = AndGate(in1[2], in2[1]);
        //    temp2[3] = AndGate(in1[3], in2[1]);
        //    temp2[4] = AndGate(in1[4], in2[1]);
        //    temp2[5] = AndGate(in1[5], in2[1]);
        //    temp2[6] = AndGate(in1[6], in2[1]);
        //    temp2[7] = AndGate(in1[7], in2[1]);

        //    temp3[0] = AndGate(in1[0], in2[2]);
        //    temp3[1] = AndGate(in1[1], in2[2]);
        //    temp3[2] = AndGate(in1[2], in2[2]);
        //    temp3[3] = AndGate(in1[3], in2[2]);
        //    temp3[4] = AndGate(in1[4], in2[2]);
        //    temp3[5] = AndGate(in1[5], in2[2]);
        //    temp3[6] = AndGate(in1[6], in2[2]);
        //    temp3[7] = AndGate(in1[7], in2[2]);

        //    temp4[0] = AndGate(in1[0], in2[3]);
        //    temp4[1] = AndGate(in1[1], in2[3]);
        //    temp4[2] = AndGate(in1[2], in2[3]);
        //    temp4[3] = AndGate(in1[3], in2[3]);
        //    temp4[4] = AndGate(in1[4], in2[3]);
        //    temp4[5] = AndGate(in1[5], in2[3]);
        //    temp4[6] = AndGate(in1[6], in2[3]);
        //    temp4[7] = AndGate(in1[7], in2[3]);

        //    temp5[0] = AndGate(in1[0], in2[4]);
        //    temp5[1] = AndGate(in1[1], in2[4]);
        //    temp5[2] = AndGate(in1[2], in2[4]);
        //    temp5[3] = AndGate(in1[3], in2[4]);
        //    temp5[4] = AndGate(in1[4], in2[4]);
        //    temp5[5] = AndGate(in1[5], in2[4]);
        //    temp5[6] = AndGate(in1[6], in2[4]);
        //    temp5[7] = AndGate(in1[7], in2[4]);

        //    temp6[0] = AndGate(in1[0], in2[5]);
        //    temp6[1] = AndGate(in1[1], in2[5]);
        //    temp6[2] = AndGate(in1[2], in2[5]);
        //    temp6[3] = AndGate(in1[3], in2[5]);
        //    temp6[4] = AndGate(in1[4], in2[5]);
        //    temp6[5] = AndGate(in1[5], in2[5]);
        //    temp6[6] = AndGate(in1[6], in2[5]);
        //    temp6[7] = AndGate(in1[7], in2[5]);

        //    temp7[0] = AndGate(in1[0], in2[6]);
        //    temp7[1] = AndGate(in1[1], in2[6]);
        //    temp7[2] = AndGate(in1[2], in2[6]);
        //    temp7[3] = AndGate(in1[3], in2[6]);
        //    temp7[4] = AndGate(in1[4], in2[6]);
        //    temp7[5] = AndGate(in1[5], in2[6]);
        //    temp7[6] = AndGate(in1[6], in2[6]);
        //    temp7[7] = AndGate(in1[7], in2[6]);

        //    temp8[0] = AndGate(in1[0], in2[7]);
        //    temp8[1] = AndGate(in1[1], in2[7]);
        //    temp8[2] = AndGate(in1[2], in2[7]);
        //    temp8[3] = AndGate(in1[3], in2[7]);
        //    temp8[4] = AndGate(in1[4], in2[7]);
        //    temp8[5] = AndGate(in1[5], in2[7]);
        //    temp8[6] = AndGate(in1[6], in2[7]);
        //    temp8[7] = AndGate(in1[7], in2[7]);

        //    return AddBytes(temp1, LeftShift(temp2, 1)); //multiply 2 first 
        //}

        static bool[] Multiply(bool[] in1, bool[] in2) //works, but better to compare with irl alu functions
        {
            int count = Digitize(in1);
            bool[] memory = new bool[8];
            Array.Copy(in2, memory, 8);

            while (count > 1)
            {
                memory = Add2Bytes(memory, in2);
                count--;
            }
            return memory;
        }

        //if 2 > x, return x
        //else x-=2
        static int Modulus(int num1, int num2) //refactor to process bool[]s
        {
            if (num1 < num2)
            {
                return num1;
            }
            return Modulus((num1 - num2), num2);
        }

        public static string format(bool[] arr) //makes bool array into formatted string of 1s and 0s
        {
            string str = "";

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (arr[i] == true)
                {
                    str += "1";
                }
                if (arr[i] == false)
                {
                    str += "0";
                }
            }

            return str;
        }

        static bool[] Binarize(int in1) //makes bool array[8] from int, works for neg in 2's comp
        {
            int parity = in1;
            bool[] outBool = new bool[8];

            for (int i = 0; i < outBool.Length; i++)
            {
                if (in1 % 2 == 0)
                {
                    outBool[i] = false;
                }
                else
                {
                    outBool[i] = true;
                    in1--;
                }
                in1 /= 2;
            }
            return outBool;
        }

        static int Digitize(bool[] in1) 
        {
            int result = 0;
            int[] parser = {1,2,4,8,16,32,64};
            for (int i = 0; i < 7; i++)
            {
                if (in1[i])
                {
                    result += parser[i];
                }
            }
            if (in1[7])
            {
                result *= -1;
            }

            return result;
        }


        /*----------------------------------------memory under construction uwu------------------------------------------------*/

        //static bool SetMem(MemBit bit, bool in1, bool set) //this is to simulate an SRLatch, evil but necessary emulation
        //{
        //    if (set)
        //    {
        //        bit.Mem = in1; //membit deleted
        //    }
        //    else
        //    {
        //        bit.Mem = bit.Mem;
        //    }
        //    return bit.Mem;
        //}

        //MemByte memByte1 = new MemByte();
        //    for (int i = 0; i<memByte1.mems.Length - 1; i++) //membyte deleted
        //    {
        //        memByte1.mems[i] = new MemBit();

        static int latch(int input, int otherInt) //causes stack overflow lol
        {
            return input + latch(input, otherInt);
        }
        
        // /////////////////////////////////////////////////////////////////////////////////////start here

        //would latch work as an object with an on/off property?
        //each irl wire is an instance, logic gates changing thir properties

        static void Main(string[] args)
        {
            /*---------------------------------------------------testing--------------------------------------------------------*/

            static void truthTable(Func<bool, bool, bool> gate) //1st time using a delegate!
            {
                Console.WriteLine(gate.Method.Name);
                Console.WriteLine("T+T= " + gate(true, true));
                Console.WriteLine("T+F= " + gate(true, false));
                Console.WriteLine("F+T= " + gate(false, true));
                Console.WriteLine("F+F= " + gate(false, false));
                Console.WriteLine();
            }
            //truthTable(NandGate);

            //8-bit registers for operations
            //7 bits plus 8th bit for signed integers
            //range= -128 to 127 inclusive

            bool[] addend1 = Binarize(23);
            bool[] addend2 = Binarize(73);
            bool[] addend3 = Binarize(-54);
            bool[] addend4 = Binarize(-74);
            bool[] addend5 = Binarize(2);
            bool[] addend6 = Binarize(3);
            bool[] addend7 = Binarize(4);
            bool[] addend8 = Binarize(5);

            //TODO:verify LeftShift

            //Console.WriteLine(format(addend1));                              //expect 00010111 // 23
            //Console.WriteLine(format(addend2));                              //expect 01001001 // 73
            //Console.WriteLine(format(addend3));                              //expect 11001010 //-54
            //Console.WriteLine(format(addend4));                              //expect 10110110 //-74
            //Console.WriteLine(format(addend5));                              //expect 00000010 // 2
            //Console.WriteLine(format(addend6));                              //expect 00000011 // 3
            //Console.WriteLine(format(addend7));                              //expect 00000100 // 4
            //Console.WriteLine(format(addend8));                              //expect 00000101 // 5
            //Console.WriteLine();
            //Console.WriteLine(format(AddBytes(addend1, addend2)));           //expect 01100000 // 96  //adds 2 positive ints
            //Console.WriteLine(format(AddBytes(addend1, addend2, addend6)));  //expect 01100011 // 99 //adds 3 positive ints (!)
            //Console.WriteLine(format(AddBytes(addend1, addend3)));           //expect 11100001 //-31  //adds 1 positive, 1 negative
            //Console.WriteLine(format(AddBytes(addend3, addend4)));           //expect 10000000 //-128 //adds 2 negative
            //Console.WriteLine();
            //Console.WriteLine(format(MakeNeg(addend1)));                     //expect 11101001 //-23 
            //Console.WriteLine(format(MakeNeg(AddBytes(addend1, addend2))));  //expect 10100000 //-96
            //Console.WriteLine(format(AddBytes(addend2, MakeNeg(addend1))));  //expect 00110010 // 50  //subtracts!
            //Console.WriteLine();
            //Console.WriteLine(format(addend5));                              //expect 00000010 // 2
            //Console.WriteLine(format(LeftShift(addend5, 1)));                //expect 00000100 // 4 
            //Console.WriteLine(format(LeftShift(addend7, 1)));                //expect 00001000 // 4
            //Console.WriteLine();
            //Console.WriteLine(format(LeftShift(addend3, 1)));                //expect 10010100 //-108
            //Console.WriteLine();
            Console.WriteLine(format(Multiply(addend5, addend5)));           //expect 00000100 // 4
            Console.WriteLine(format(Multiply(addend1, addend5)));           //expect 00101110 // 46
            Console.WriteLine(format(Multiply(addend5, addend6)));           //expect 00000110 // 6 
            Console.WriteLine(format(Multiply(addend1, addend6)));           //expect 01000101 // 69
            Console.WriteLine(format(Multiply(addend7, addend5)));           //expect 00001000 // 8  // works unless 4 is 2nd number?
            Console.WriteLine(format(Multiply(addend7, addend8)));           //expect 00010100 // 20 // 4?
            Console.WriteLine(format(Multiply(addend8, addend5)));           //expect 00001010 // 10
            //Console.WriteLine(format(Multiply(Binarize(4), Binarize(3))));   //00001100
            //Console.WriteLine();
            //Console.WriteLine(format(Add2Bytes(Binarize(10), MakeNeg(Binarize(7)))));   //expect 00000011
            //Console.WriteLine(format(AddBytes(Binarize(3), Binarize(4), Binarize(8)))); //expect 00001111
            //Console.WriteLine(format(Binarize(-6)));                          //expect 11111010 //-6 
            //Console.WriteLine(format(MakeNeg(Binarize(6))));                  //expect 11111010 //-6
            //Console.WriteLine((Modulus(27, 2)));
            Console.WriteLine(format(Multiply(addend1, addend8)));              //expect 0110011
            Console.WriteLine(Digitize(addend3));
        }
    }
}
