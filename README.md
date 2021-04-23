# Find BadChars

This is a simple application that will automate the process of finding bad characters for a simple Windows based buffer overflow.

# Download
Latest Release: https://github.com/versex799/OSCP_BadChars/releases/tag/V1

# Process

After sending the bad characters sequence to the service, you will will need to copy the dump output from the Immunity debugger. 
To do this, simply right click on ESP in the top right quadrant of the debugger and select "Follow In Dump". You will see the
output in the lower left quadrant. Highligt all the characters in the dump from 01 to FF. Right click it and select Copy To Clipboard.


![Follow In Dump](https://github.com/versex799/OSCP_BadChars/blob/master/follow.png)


![Immunity Dump](https://github.com/versex799/OSCP_BadChars/blob/master/dump.png)


You can then paste this output into a text file and save it to the computer running the Find BadChars application. 


![Dump Text File](https://github.com/versex799/OSCP_BadChars/blob/master/bc.png)


In the application select the text file you just created and hit the "Get Bad Chars" button. The bottom box will display all the bad characters found in
the output.


![Application Output](https://github.com/versex799/OSCP_BadChars/blob/master/badChars.png)
