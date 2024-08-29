using System.Text;
// Check at programmet har fået tildelt en config fil.
int len = args.Length;
if (len == 0) {
    Console.WriteLine("Supply a configuration file, when running the program.");
    return 1;
} else if (len > 1) {
    Console.WriteLine("Please only supply one configuration file (given {0} files)", len);
    return 1;
}

string f = args[0];
string output = "test.sh";

string[] rows = File.ReadAllLines(f);
StringBuilder testContent = new StringBuilder();
testContent.AppendLine("#!/bin/bash");

for (int i = 0; i < rows.Length; i++) {

    testContent.AppendLine("ACTUAL=$(mktemp)");
    testContent.AppendLine("EXPECTED=$(mktemp)");
    string[] data = rows[i].Split(',');
    string inputType = data[1];
    string P = data[0];
    string D = data[2];
    string R = data[3];
    switch (inputType) {
        case "STDIN":
            testContent.AppendLine($"echo \"{R}\" > $EXPECTED");
            testContent.AppendLine($"echo \"{D}\" | ./{P} > $ACTUAL");
            testContent.AppendLine($"if ! diff -q $ACTUAL $EXPECTED");
            testContent.AppendLine("then");
            testContent.AppendLine($"  echo \"Failed test of {P} with standard input {D}\"");
            testContent.AppendLine("fi");
            break;

        case "ARGS":
            testContent.AppendLine($"echo \"{R}\" > $EXPECTED");
            testContent.AppendLine($"./{P} \"{D}\" > $ACTUAL");
            testContent.AppendLine("if ! diff -q $ACTUAL $EXPECTED");
            testContent.AppendLine("then");
            testContent.AppendLine($"  echo \"Failed test of {P} with standard input {D}\"");
            testContent.AppendLine("fi");
            break;

        case "FILE":
            testContent.AppendLine("TMPFILE=$(mktemp)");
            testContent.AppendLine($"echo \"{D}\" > $TMPFILE");
            testContent.AppendLine($"echo \"{R}\" > $EXPECTED");
            testContent.AppendLine($"./{P} $TMPFILE > $ACTUAL");
            testContent.AppendLine("if ! diff -q $ACTUAL $EXPECTED");
            testContent.AppendLine("then");
            testContent.AppendLine($"  echo \"Failed test of {P} with standard input {D}\"");
            testContent.AppendLine("fi");
            break;
    }
}
File.WriteAllText(output, testContent.ToString());
Console.WriteLine("Test content succesfully writen to test.sh");
return 0;