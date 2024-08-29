#!/bin/bash
ACTUAL=$(mktemp)
EXPECTED=$(mktemp)
echo "Result: 4" > $EXPECTED
echo "2+2" | ./calc > $ACTUAL
if ! diff -q $ACTUAL $EXPECTED
then
  echo "Failed test of calc with standard input 2+2"
fi
ACTUAL=$(mktemp)
EXPECTED=$(mktemp)
echo "Result: 2" > $EXPECTED
./calc "5-3" > $ACTUAL
if ! diff -q $ACTUAL $EXPECTED
then
  echo "Failed test of calc with standard input 5-3"
fi
ACTUAL=$(mktemp)
EXPECTED=$(mktemp)
TMPFILE=$(mktemp)
echo "10+5-3" > $TMPFILE
echo "Result: 12" > $EXPECTED
./calc $TMPFILE > $ACTUAL
if ! diff -q $ACTUAL $EXPECTED
then
  echo "Failed test of calc with standard input 10+5-3"
fi
ACTUAL=$(mktemp)
EXPECTED=$(mktemp)
echo "Result: 4" > $EXPECTED
echo "2+2" | ./calc > $ACTUAL
if ! diff -q $ACTUAL $EXPECTED
then
  echo "Failed test of calc with standard input 2+2"
fi
ACTUAL=$(mktemp)
EXPECTED=$(mktemp)
echo "Result: 579" > $EXPECTED
./calc "123+456" > $ACTUAL
if ! diff -q $ACTUAL $EXPECTED
then
  echo "Failed test of calc with standard input 123+456"
fi
ACTUAL=$(mktemp)
EXPECTED=$(mktemp)
TMPFILE=$(mktemp)
echo "10" > $TMPFILE
echo "Result: 10" > $EXPECTED
./calc $TMPFILE > $ACTUAL
if ! diff -q $ACTUAL $EXPECTED
then
  echo "Failed test of calc with standard input 10"
fi
