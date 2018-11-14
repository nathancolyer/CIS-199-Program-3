// Program 3
// CIS 199-01
// Due: 4/13/17
// By: A1212

// This application uses BEAUTIFUL and PERFECT existing code and calculates the earliest registration date
// and time for an undergraduate student given their class standing
// and last name using parallel arrays and range match.
// Decisions based on UofL Fall/Summer 2017 Priority Registration Schedule

// Solution 3
// This solution keeps the first letter of the last name as a char
// and uses if/else logic for the times.
// It uses defined strings for the dates and times to make it easier
// to maintain.
// It only uses programming elements introduced in the text or
// in class.
// This solution takes advantage of the fact that there really are
// only two different time patterns used. One for juniors and seniors
// and one for sophomores and freshmen. The pattern for sophomores
// and freshmen is complicated by the fact the certain letter ranges
// get one date and other letter ranges get another date.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        // Find and display earliest registration time
        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string DAY1 = "March 29";  // 1st day of registration
            const string DAY2 = "March 30";  // 2nd day of registration
            const string DAY3 = "March 31";  // 3rd day of registration
            const string DAY4 = "April 3";   // 4th day of registration
            const string DAY5 = "April 4";   // 5th day of registration
            const string DAY6 = "April 5";   // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            bool isUpperClass;        // Upperclass or not?

            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                {
                    isUpperClass = (seniorRBtn.Checked || juniorRBtn.Checked);

                    // Juniors and Seniors share same schedule but different days
                    if (isUpperClass)
                    {
                        char[] letterRange = { 'A', 'E', 'J', 'P', 'T' }; // seniors and juniors char range array
                        string[] timeSlot = { TIME3, TIME4, TIME5, TIME1, TIME2 }; // times match with each range of letters
                        bool found = false;


                        if (seniorRBtn.Checked)
                            dateStr = DAY1;
                        else // Must be juniors
                            dateStr = DAY2;

                        int index = letterRange.Length - 1;

                        while (index >= 0 && !found)
                        {
                            if (lastNameLetterCh >= letterRange[index]) //Compares unicode of char and compares to lowest value in range.
                                found = true;
                            else
                                --index;
                        }

                        if (found)
                            timeStr = timeSlot[index]; // sets string equal to string in array index position

                    }
                    // Sophomores and Freshmen
                    else // Must be soph/fresh
                    {
                        if (sophomoreRBtn.Checked)
                        {
                            // C-O on one day
                            if ((lastNameLetterCh >= 'C') && // >= C and
                                (lastNameLetterCh <= 'O'))   // <= O
                                dateStr = DAY4;
                            else // All other letters on previous day
                                dateStr = DAY3;
                        }
                        else // must be freshman
                        {
                            // C-O on one day
                            if ((lastNameLetterCh >= 'C') && // >= C and
                                (lastNameLetterCh <= 'O'))   // <= O
                                dateStr = DAY6;
                            else // All other letters on previous day
                                dateStr = DAY5;
                        }

                        char[] letterRange = { 'A', 'C','E','G','J', 'M', 'P', 'R','T','W' }; // sophomore and freshman char range array
                        string[] timeSlot = { TIME5,TIME1,TIME2,TIME3,TIME4,TIME5,TIME1,TIME2,TIME3,TIME4 }; // times match up with letter range
                        bool found = false;

                        int index = letterRange.Length - 1;

                        while (index >= 0 && !found)
                        {
                            if (lastNameLetterCh >= letterRange[index])
                                found = true;
                            else
                                --index;
                        }

                        if (found)
                            timeStr = timeSlot[index];
                    }

                    // Output results
                    dateTimeLbl.Text = dateStr + " at " + timeStr;
                }
                else // First char not a letter
                    MessageBox.Show("Make sure last name starts with a letter");
            }
            else // Empty textbox
                MessageBox.Show("Enter a last name!");
        }
    }
}
