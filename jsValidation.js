////////////////////Browser detection function ver=1////////////////////
 function browserDetect(which)
    {    
       NS4 = (document.layers);
       IE4 = (document.all);
      ver4 = (NS4 || IE4);   
     isMac = (navigator.appVersion.indexOf("Mac") != -1);
     isMenu = (NS4 || (IE4 && !isMac));
    }
     
////////////////////End of Browser detection function ver=1////////////////////

/////////////True/False Functions/////////////

var digits = "0123456789";
var lowercaseLetters = "abcdefghijklmnopqrstuvwxyz"
var uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

// whitespace characters
var whitespace = " \t\n\r";

// decimal point character differs by language and culture
var decimalPointDelimiter = "."

// non-digit characters which are allowed in phone numbers
var phoneNumberDelimiters = "()- ";

// characters which are allowed in US phone numbers
var validUSPhoneChars = digits + phoneNumberDelimiters;

// characters which are allowed in international phone numbers
// (a leading + is OK)
var validWorldPhoneChars = digits + phoneNumberDelimiters + "+";

// non-digit characters which are allowed in 
// Social Security Numbers
var SSNDelimiters = "- "

// characters which are allowed in Social Security Numbers
var validSSNChars = digits + SSNDelimiters;

// U.s. Social Security Numbers have 9 digits.
// They are formatted as 123-45-6789.
var digitsInSocialSecurityNumber = 9;

// U.s. phone numbers have 10 digits.
// They are formatted as 123 456 7890 or (123) 456-7890.
var digitsInUSPhoneNumber = 10;

// non-digit characters which are allowed in ZIP Codes
var ZIPCodeDelimiters = "-";

// our preferred delimiter for reformatting ZIP Codes
var ZIPCodeDelimeter = "-"

// characters which are allowed in Social Security Numbers
var validZIPCodeChars = digits + ZIPCodeDelimiters

// U.s. ZIP codes have 5 or 9 digits.
// They are formatted as 12345 or 12345-6789.
var digitsInZIPCode1 = 5
var digitsInZIPCode2 = 9

// CONSTANT STRING DECLARATIONS
// (grouped for ease of translation and localization)

var daysInMonth = makeArray(12);
daysInMonth[1] = 31;
daysInMonth[2] = 29;   // must programmatically check this
daysInMonth[3] = 31;
daysInMonth[4] = 30;
daysInMonth[5] = 31;
daysInMonth[6] = 30;
daysInMonth[7] = 31;
daysInMonth[8] = 31;
daysInMonth[9] = 30;
daysInMonth[10] = 31;
daysInMonth[11] = 30;
daysInMonth[12] = 31;

// Valid U.s. Postal Codes for states, territories, armed forces, etc.
var USStateCodeDelimiter = "|";
var USStateCodes = "AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY|AE|AA|AE|AE|AP"

//sets defaultEmptyOk to False
var defaultEmptyOK = false

// Check whether string s is empty.
function isEmpty(str) {   
  return ((str == null) || (str.length == 0))
}

// whitespace characters only.
// Returns true if string s is empty or 
function isWhitespace (str) {
  var i;
  // Is s empty?
  if (isEmpty(str)) return true;
    // Search through string's characters one by one
    // until we find a non-whitespace character.
    // When we do, return false; if we don't, return true.
      for (i = 0; i < str.length; i++) {   
        // Check that current character isn't whitespace.
        var c = str.charAt(i);
          if (whitespace.indexOf(c) == -1) return false;
      }
    // All characters are whitespace.
    return true;
}

// WORKAROUND FUNCTION FOR NAVIGATOR 2.0.2 COMPATIBILITY.
//
// The below function *should* be unnecessary.  In general,
// avoid using it.  Use the standard method indexOf instead.
//
// However, because of an apparent bug in indexOf on 
// Navigator 2.0.2, the below loop does not work as the
// body of stripInitialWhitespace:
//
// while ((i < str.length) && (whitespace.indexOf(str.charAt(i)) != -1))
//   i++;
//
// ... so we provide this workaround function charInString
// instead.
//
// charInString (CHARACTER c, STRING s)
//
// Returns true if single character c (actually a string)
// is contained within string str.

function charInString (char, str) {
  for (i = 0; i < str.length; i++) {
    if (str.charAt(i) == char) return true;
  }
  return false
}

// Returns true if character c is an English letter 
// (A .. Z, a..z).
//
// NOTE: Need i18n version to support European characters.
// This could be tricky due to different character
// sets and orderings for various languages and platforms.

function isLetter (char) {
  return ( ((char >= "a") && (char <= "z")) || ((char >= "A") && (char <= "Z")) )
}

// Returns true if character c is a digit 
// (0 .. 9).

function isDigit (char) {
  return ((char >= "0") && (char <= "9"))
}

// Returns true if character c is a letter or digit.

function isLetterOrDigit (char) {   
  return (isLetter(char) || isDigit(char))
}

// isInteger (STRING s [, BOOLEAN emptyOK])
// 
// Returns true if all characters in string s are numbers.
//
// Accepts non-signed integers only. Does not accept floating 
// point, exponential notation, etc.
//
// We don't use parseInt because that would accept a string
// with trailing non-numeric characters.
//
// By default, returns defaultEmptyOK if s is empty.
// There is an optional second argument called emptyOK.
// emptyOK is used to override for a single function call
//      the default behavior which is specified globally by
//      defaultEmptyOK.
// If emptyOK is false (or any value other than true), 
//      the function will return false if s is empty.
// If emptyOK is true, the function will return true if s is empty.
//
// EXAMPLE FUNCTION CALL:     RESULT:
// isInteger ("5")            true 
// isInteger ("")             defaultEmptyOK
// isInteger ("-5")           false
// isInteger ("", true)       true
// isInteger ("", false)      false
// isInteger ("5", false)     true

function isInteger (str) {
  var i;
  if (isEmpty(str)) 
    if (isInteger.arguments.length == 1) return defaultEmptyOK;
    else return (isInteger.arguments[1] == true);

    // Search through string's characters one by one
    // until we find a non-numeric character.
    // When we do, return false; if we don't, return true.

    for (i = 0; i < str.length; i++) {   
      // Check that current character is number.
      var c = str.charAt(i);
      if (!isDigit(c)) return false;
    }
    // All characters are numbers.
    return true;
}

// isSignedInteger (STRING s [, BOOLEAN emptyOK])
// 
// Returns true if all characters are numbers; 
// first character is allowed to be + or - as well.
//
// Does not accept floating point, exponential notation, etc.
//
// We don't use parseInt because that would accept a string
// with trailing non-numeric characters.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.
//
// EXAMPLE FUNCTION CALL:          RESULT:
// isSignedInteger ("5")           true 
// isSignedInteger ("")            defaultEmptyOK
// isSignedInteger ("-5")          true
// isSignedInteger ("+5")          true
// isSignedInteger ("", false)     false
// isSignedInteger ("", true)      true

function isSignedInteger (str) {
  if (isEmpty(str)) 
    if (isSignedInteger.arguments.length == 1) return defaultEmptyOK;
    else return (isSignedInteger.arguments[1] == true);
  else {
    var startPos = 0;
    var secondArg = defaultEmptyOK;
    if (isSignedInteger.arguments.length > 1)
      secondArg = isSignedInteger.arguments[1];
      // skip leading + or -
      if ( (str.charAt(0) == "-") || (str.charAt(0) == "+") )
        startPos = 1;    
        return (isInteger(str.substring(startPos, str.length), secondArg))
    }
}

// isPositiveInteger (STRING s [, BOOLEAN emptyOK])
// 
// Returns true if string s is an integer > 0.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isPositiveInteger (str) {
  var secondArg = defaultEmptyOK;
  if (isPositiveInteger.arguments.length > 1)
    secondArg = isPositiveInteger.arguments[1];
    // b) one of the following must b
    // The next line is a bit byzantine.  What it means is:
    // a) str must be a signed integer, ANDe true:
    //    i)  s is empty and we are supposed to return true for
    //        empty strings
    //    ii) this is a positive, not negative, number
    return (isSignedInteger(str, secondArg) && ( (isEmpty(str) && secondArg)  || (parseInt (str) > 0) ) );
}

// isNonnegativeInteger (STRING s [, BOOLEAN emptyOK])
// 
// Returns true if string s is an integer >= 0.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isNonnegativeInteger (str) {
  var secondArg = defaultEmptyOK;
  if (isNonnegativeInteger.arguments.length > 1)
    secondArg = isNonnegativeInteger.arguments[1];
    // The next line is a bit byzantine.  What it means is:
    // a) s must be a signed integer, AND
    // b) one of the following must be true:
    //    i)  str is empty and we are supposed to return true for
    //        empty strings
    //    ii) this is a number >= 0
    return (isSignedInteger(str, secondArg)
         && ( (isEmpty(str) && secondArg)  || (parseInt (str) >= 0) ) );
}

// isNegativeInteger (STRING s [, BOOLEAN emptyOK])
// 
// Returns true if string s is an integer < 0.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isNegativeInteger (str) {
  var secondArg = defaultEmptyOK;
  if (isNegativeInteger.arguments.length > 1)
    secondArg = isNegativeInteger.arguments[1];
    // The next line is a bit byzantine.  What it means is:
    // a) s must be a signed integer, AND
    // b) one of the following must be true:
    //    i)  s is empty and we are supposed to return true for
    //        empty strings
    //    ii) this is a negative, not positive, number
    return (isSignedInteger(str, secondArg)
         && ( (isEmpty(str) && secondArg)  || (parseInt (str) < 0) ) );
}

// isNonpositiveInteger (STRING s [, BOOLEAN emptyOK])
// 
// Returns true if string s is an integer <= 0.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isNonpositiveInteger (str) {
  var secondArg = defaultEmptyOK;
  if (isNonpositiveInteger.arguments.length > 1)
    secondArg = isNonpositiveInteger.arguments[1];
    // The next line is a bit byzantine.  What it means is:
    // a) s must be a signed integer, AND
    // b) one of the following must be true:
    //    i)  s is empty and we are supposed to return true for
    //        empty strings
    //    ii) this is a number <= 0

    return (isSignedInteger(str, secondArg)
         && ( (isEmpty(str) && secondArg)  || (parseInt (str) <= 0) ) );
}

// isFloat (STRING s [, BOOLEAN emptyOK])
// 
// True if string s is an unsigned floating point (real) number. 
//
// Also returns true for unsigned integers. If you wish
// to distinguish between integers and floating point numbers,
// first call isInteger, then call isFloat.
//
// Does not accept exponential notation.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isFloat (str) { 
  var i;
  var seenDecimalPoint = false;
  if (isEmpty(str)) 
    if (isFloat.arguments.length == 1) return defaultEmptyOK;
    else return (isFloat.arguments[1] == true);
    if (str == decimalPointDelimiter) return false;
    // Search through string's characters one by one
    // until we find a non-numeric character.
    // When we do, return false; if we don't, return true.
    for (i = 0; i < str.length; i++)
    {   
      // Check that current character is number.
      var c = str.charAt(i);
      if ((c == decimalPointDelimiter) && !seenDecimalPoint) seenDecimalPoint = true;
      else if (!isDigit(c)) return false;
    }
    // All characters are numbers.
    return true;
}

// isSignedFloat (STRING s [, BOOLEAN emptyOK])
// 
// True if string s is a signed or unsigned floating point 
// (real) number. First character is allowed to be + or -.
//
// Also returns true for unsigned integers. If you wish
// to distinguish between integers and floating point numbers,
// first call isSignedInteger, then call isSignedFloat.
//
// Does not accept exponential notation.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isSignedFloat (str) {
  if (isEmpty(str)) 
    if (isSignedFloat.arguments.length == 1) return defaultEmptyOK;
    else return (isSignedFloat.arguments[1] == true);
  else {
    var startPos = 0;
    var secondArg = defaultEmptyOK;
      if (isSignedFloat.arguments.length > 1)
        secondArg = isSignedFloat.arguments[1];
        // skip leading + or -
        if ( (str.charAt(0) == "-") || (str.charAt(0) == "+") )
           startPos = 1;    
        return (isFloat(str.substring(startPos, strlength), secondArg))
  }
}

// isAlphabetic (STRING s [, BOOLEAN emptyOK])
// 
// Returns true if string s is English letters 
// (A .. Z, a..z) only.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.
//
// NOTE: Need i18n version to support European characters.
// This could be tricky due to different character
// sets and orderings for various languages and platforms.

function isAlphabetic (str) {
  var i;
  if (isEmpty(str)) 
    if (isAlphabetic.arguments.length == 1) return defaultEmptyOK;
    else return (isAlphabetic.arguments[1] == true);
    // Search through string's characters one by one
    // until we find a non-alphabetic character.
    // When we do, return false; if we don't, return true.
    for (i = 0; i < str.length; i++) {   
        // Check that current character is letter.
        var c = str.charAt(i);
        if (!isLetter(c))
        return false;
    }
    // All characters are letters.
    return true;
}

// isAlphanumeric (STRING s [, BOOLEAN emptyOK])
// 
// Returns true if string s is English letters 
// (A .. Z, a..z) and numbers only.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.
//
// NOTE: Need i18n version to support European characters.
// This could be tricky due to different character
// sets and orderings for various languages and platforms.

function isAlphanumeric (str) {
  var i;
  if (isEmpty(str)) 
    if (isAlphanumeric.arguments.length == 1) return defaultEmptyOK;
    else return (isAlphanumeric.arguments[1] == true);
    // Search through string's characters one by one
    // until we find a non-alphanumeric character.
    // When we do, return false; if we don't, return true.
    for (i = 0; i < str.length; i++) {   
      // Check that current character is number or letter.
      var c = str.charAt(i);
      if (! (isLetter(c) || isDigit(c) ) )
        return false;
  }
    // All characters are numbers or letters.
    return true;
}

// isSSN (STRING s [, BOOLEAN emptyOK])
// 
// isSSN returns true if string s is a valid U.s. Social
// Security Number.  Must be 9 digits.
//
// NOTE: Strip out any delimiters (spaces, hyphens, etc.)
// from string s before calling this function.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isSSN (str) {
  if (isEmpty(str)) 
    if (isSSN.arguments.length == 1) return defaultEmptyOK;
    else return (isSSN.arguments[1] == true);
    return (isInteger(str) && str.length == digitsInSocialSecurityNumber)
} 

// isUSPhoneNumber (STRING s [, BOOLEAN emptyOK])
// 
// isUSPhoneNumber returns true if string s is a valid U.s. Phone
// Number.  Must be 10 digits.
//
// NOTE: Strip out any delimiters (spaces, hyphens, parentheses, etc.)
// from string s before calling this function.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isUSPhoneNumber (str) {
  if (isEmpty(str)) 
    if (isUSPhoneNumber.arguments.length == 1) return defaultEmptyOK;
    else return (isUSPhoneNumber.arguments[1] == true);
    return (isInteger(str) && str.length == digitsInUSPhoneNumber)
}

// isInternationalPhoneNumber (STRING s [, BOOLEAN emptyOK])
// 
// isInternationalPhoneNumber returns true if string s is a valid 
// international phone number.  Must be digits only; any length OK.
// May be prefixed by + character.
//
// NOTE: A phone number of all zeros would not be accepted.
// I don't think that is a valid phone number anyway.
//
// NOTE: Strip out any delimiters (spaces, hyphens, parentheses, etc.)
// from string s before calling this function.  You may leave in 
// leading + character if you wish.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isInternationalPhoneNumber (str) {
  if (isEmpty(str)) 
    if (isInternationalPhoneNumber.arguments.length == 1) return defaultEmptyOK;
    else return (isInternationalPhoneNumber.arguments[1] == true);
    return (isPositiveInteger(str))
}

// isZIPCode (STRING s [, BOOLEAN emptyOK])
// 
// isZIPCode returns true if string s is a valid 
// U.str. ZIP code.  Must be 5 or 9 digits only.
//
// NOTE: Strip out any delimiters (spaces, hyphens, etc.)
// from string s before calling this function.  
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isZIPCode (str) {
  if (isEmpty(str)) 
    if (isZIPCode.arguments.length == 1) return defaultEmptyOK;
    else return (isZIPCode.arguments[1] == true);
  return (isInteger(str) && 
    ((str.length == digitsInZIPCode1) ||
    (str.length == digitsInZIPCode2)))
}

// isStateCode (STRING s [, BOOLEAN emptyOK])
// 
// Return true if s is a valid U.s. Postal Code 
// (abbreviation for state).
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isStateCode(str) {
  if (isEmpty(str)) 
    if (isStateCode.arguments.length == 1) return defaultEmptyOK;
       else return (isStateCode.arguments[1] == true);
    return ( (USStateCodes.indexOf(str) != -1) &&
             (str.indexOf(USStateCodeDelimiter) == -1) )
}

// isYear (STRING s [, BOOLEAN emptyOK])
// 
// isYear returns true if string s is a valid 
// Year number.  Must be 2 or 4 digits only.
// 
// For Year 2000 compliance, you are advised
// to use 4-digit year numbers everywhere.
//
// And yes, this function is not Year 10000 compliant, but 
// because I am giving you 8003 years of advance notice,
// I don't feel very guilty about this ...
//
// For B.C. compliance, write your own function. ;->
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isYear (str) {
  if (isEmpty(str)) 
    if (isYear.arguments.length == 1) return defaultEmptyOK;
    else return (isYear.arguments[1] == true);
    if (!isNonnegativeInteger(str)) return false;
      return ((str.length == 2) || (str.length == 4));
}

// isIntegerInRange (STRING s, INTEGER a, INTEGER b [, BOOLEAN emptyOK])
// 
// isIntegerInRange returns true if string s is an integer 
// within the range of integer arguments a and b, inclusive.
// 
// For explanation of optional argument emptyOK,
// see comments of function isInteger.


function isIntegerInRange (str, a, b) {
if (isEmpty(str)) 
  if (isIntegerInRange.arguments.length == 1) return defaultEmptyOK;
  else return (isIntegerInRange.arguments[1] == true);
  // Catch non-integer strings to avoid creating a NaN below,
  // which isn't available on JavaScript 1.0 for Windows.
  if (!isInteger(str, false)) return false;
    // Now, explicitly change the type to integer via parseInt
    // so that the comparison code below will work both on 
    // JavaScript 1.2 (which typechecks in equality comparisons)
    // and JavaScript 1.1 and before (which doesn't).
    var num = parseInt (str);
    return ((num >= a) && (num <= b));
}

// isMonth (STRING s [, BOOLEAN emptyOK])
// 
// isMonth returns true if string s is a valid 
// month number between 1 and 12.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isMonth (str) {
  if (isEmpty(str)) 
    if (isMonth.arguments.length == 1) return defaultEmptyOK;
    else return (isMonth.arguments[1] == true);
       return isIntegerInRange (s, 1, 12);
}

// isDay (STRING s [, BOOLEAN emptyOK])
// 
// isDay returns true if string s is a valid 
// day number between 1 and 31.
// 
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isDay (str) {
  if (isEmpty(str)) 
    if (isDay.arguments.length == 1) return defaultEmptyOK;
    else return (isDay.arguments[1] == true);   
      return isIntegerInRange (s, 1, 31);
}

function isTime(timeStr) {
  // Checks if time is in HH:MM:SS AM/PM format.
  // The seconds and AM/PM are optional.
  var timePat = /^(\d{1,2}):(\d{2})(:(\d{2}))?(\s?(AM|am|PM|pm))?$/;
  var matchArray = timeStr.match(timePat);
  if (matchArray == null) {
    return false;
  }
  hour = matchArray[1];
  minute = matchArray[2];
  second = matchArray[4];
  ampm = matchArray[6];
  if (second=="") { second = null; }
  if (ampm=="") { ampm = null }
  if (hour < 0  || hour > 23) {
    return false;
  }
  if (hour <= 12 && ampm == null) {
    return false;
  }
  if  (hour > 12 && ampm != null) {
    return false;
  }
  if (minute<0 || minute > 59) {
    return false;
  }
  if (second != null && (second < 0 || second > 59)) {
    return false;
  }
return true;
}




//*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*




// checkString (TEXTFIELD theField, STRING s, [, BOOLEAN emptyOK==false])
//
// Check that string theField.value is not all whitespace.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function checkString (theField, str, emptyOK) {
  // Next line is needed on NN3 to avoid "undefined is not a number" error
  // in equality comparison below.
  if (checkString.arguments.length == 2) emptyOK = defaultEmptyOK;
  if ((emptyOK == true) && (isEmpty(theField.value))) return true;
  if (isWhitespace(theField.value)) 
    return false;
  else return true;
}

// checkStateCode (TEXTFIELD theField [, BOOLEAN emptyOK==false])
//
// Check that string theField.value is a valid U.s. state code.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function checkStateCode (theField, emptyOK) {
  if (checkStateCode.arguments.length == 1) emptyOK = defaultEmptyOK;
  if ((emptyOK == true) && (isEmpty(theField.value))) return true;
  else {
    theField.value = theField.value.toUpperCase();
    if (!isStateCode(theField.value, false)) 
      return false;
    else return true;
  }
}

// checkZIPCode (TEXTFIELD theField [, BOOLEAN emptyOK==false])
//
// Check that string theField.value is a valid ZIP code.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function checkZIPCode (theField, emptyOK) {
  if (checkZIPCode.arguments.length == 1) emptyOK = defaultEmptyOK;
  if ((emptyOK == true) && (isEmpty(theField.value))) return true;
  else {
    var normalizedZIP = stripCharsInBag(theField.value, ZIPCodeDelimiters)
    if (!isZIPCode(normalizedZIP, false)) 
      return false;
    else {
      // if you don't want to insert a hyphen, comment next line out
      //theField.value = reformatZIPCode(normalizedZIP)
      return true;
    }
  }
}

// checkUSPhone (TEXTFIELD theField [, BOOLEAN emptyOK==false])
//
// Check that string theField.value is a valid US Phone.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function checkUSPhone (theField, emptyOK) {
  if (checkUSPhone.arguments.length == 1) emptyOK = defaultEmptyOK;
  if ((emptyOK == true) && (isEmpty(theField.value))) return true;
  else {
    var normalizedPhone = stripCharsInBag(theField.value, phoneNumberDelimiters)
    if (!isUSPhoneNumber(normalizedPhone, false)) 
      return false;
    else {
      // if you don't want to reformat as (123) 456-789, comment next line out
      //theField.value = reformatUSPhone(normalizedPhone)
      return true;
    }
  }
}

// checkInternationalPhone (TEXTFIELD theField [, BOOLEAN emptyOK==false])
//
// Check that string theField.value is a valid International Phone.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function checkInternationalPhone (theField, emptyOK) {
  if (checkInternationalPhone.arguments.length == 1) emptyOK = defaultEmptyOK;
  if ((emptyOK == true) && (isEmpty(theField.value))) return true;
  else {
    if (!isInternationalPhoneNumber(theField.value, false)) 
      return false;
    else return true;
  }
}

// Check that string theField.value is a valid SSN.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function checkSSN (theField, emptyOK) {
  if (checkSSN.arguments.length == 1) emptyOK = defaultEmptyOK;
  if ((emptyOK == true) && (isEmpty(theField.value))) return true;
  else {
    var normalizedSSN = stripCharsInBag(theField.value, SSNDelimiters)
    if (!isSSN(normalizedSSN, false)) 
      return false;
    else {
      // if you don't want to reformats as 123-456-7890, comment next line out
      //theField.value = reformatSSN(normalizedSSN)
      return true;
    }
  }
}

// Check that string theField.value is a valid Year.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function checkYear (theField, emptyOK) {
  if (checkYear.arguments.length == 1) emptyOK = defaultEmptyOK;
  if ((emptyOK == true) && (isEmpty(theField.value))) return true;
  if (!isYear(theField.value, false)) 
    return false;
  else return true;
}

// Check that string theField.value is a valid Month.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function checkMonth (theField, emptyOK) {
  if (checkMonth.arguments.length == 1) emptyOK = defaultEmptyOK;
  if ((emptyOK == true) && (isEmpty(theField.value))) return true;
  if (!isMonth(theField.value, false)) 
    return false;
  else return true;
}

// Check that string theField.value is a valid Day.
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function checkDay (theField, emptyOK) {
  if (checkDay.arguments.length == 1) emptyOK = defaultEmptyOK;
  if ((emptyOK == true) && (isEmpty(theField.value))) return true;
  if (!isDay(theField.value, false)) 
    return false;
  else return true;
}

// checkDate (yearField, monthField, dayField, STRING labelString [, OKtoOmitDay==false])
//
// Check that yearField.value, monthField.value, and dayField.value 
// form a valid date.
//
// If they don't, labelString (the name of the date, like "Birth Date")
// is displayed to tell the user which date field is invalid.
//
// If it is OK for the day field to be empty, set optional argument
// OKtoOmitDay to true.  It defaults to false.

function checkDate (yearField, monthField, dayField, str) {
  // Next line is needed on NN3 to avoid "undefined is not a number" error
  // in equality comparison below.
  if (checkDate.arguments.length == 3) {
    if (!isYear(yearField.value)) return false;
    if (!isMonth(monthField.value)) return false;
    if (isEmpty(dayField.value)) return true;
    else if (!isDay(dayField.value)) 
      return false;
  }
  if (isDate (yearField.value, monthField.value, dayField.value))
    return true;
   
}

// isDate (STRING year, STRING month, STRING day)
//
// isDate returns true if string arguments year, month, and day 
// form a valid date.
// 

function isDate (year, month, day)
{   // catch invalid years (not 2- or 4-digit) and invalid months and days.
    if (! (isYear(year, false) && isMonth(month, false) && isDay(day, false))) return false;

    // Explicitly change type to integer to make code work in both
    // JavaScript 1.1 and JavaScript 1.2.
    var intYear = parseInt(year);
    var intMonth = parseInt(month);
    var intDay = parseInt(day);

    // catch invalid days, except for February
    if (intDay > daysInMonth[intMonth]) return false; 

    if ((intMonth == 2) && (intDay > daysInFebruary(intYear))) return false;

    return true;
}

/*  //function grabs the date then sends it to the chkdate function
function isDate(str) {
  var datefield = str;     
  if (chkdate(str) == false) { 
   return false;
  }else {
   return true;
  }
} 

//function to check format of the Date entry
function chkdate(str) {
  var strDatestyle = "US"; //United States date style
  var strDate;
  var strDateArray;
  var strDay;
  var strMonth;
  var strYear;
  var intday;
  var intMonth;
  var intYear;
  var booFound = false;
  var datefield = str;
  var strSeparatorArray = new Array("-"," ","/",".");
  var intElementNr;
  var err = 0;
  var strMonthArray = new Array(12);
  strMonthArray[0] = "Jan";
  strMonthArray[1] = "Feb";
  strMonthArray[2] = "Mar";
  strMonthArray[3] = "Apr";
  strMonthArray[4] = "May";
  strMonthArray[5] = "Jun";
  strMonthArray[6] = "Jul";
  strMonthArray[7] = "Aug";
  strMonthArray[8] = "Sep";
  strMonthArray[9] = "Oct";
  strMonthArray[10] = "Nov";
  strMonthArray[11] = "Dec";
  strDate = datefield.value;
  if (strDate.length < 1) {
   return false;
  }
  for (intElementNr = 0; intElementNr < strSeparatorArray.length; intElementNr++) {
   if (strDate.indexOf(strSeparatorArray[intElementNr]) != -1) {
     strDateArray = strDate.split(strSeparatorArray[intElementNr]);
     if (strDateArray.length != 3) {
       err = 1;
       return false;
     }else {
       strDay = strDateArray[0];
       strMonth = strDateArray[1];
       strYear = strDateArray[2];
     }
     booFound = true;
   }
  }
  if (booFound == false) {
    if (strDate.length>5) {
      strDay = strDate.substr(0, 2);
      strMonth = strDate.substr(2, 2);
      strYear = strDate.substr(4);
    }
  }
  if (strYear.length == 2) {
    strYear = '20' + strYear;
  }else { 
    return false; 
  } 
  // US style
  if (strDatestyle == "US") {
    strTemp = strDay;
    strDay = strMonth;
    strMonth = strTemp;
  }
  intday = parseInt(strDay, 10);
  if (isNaN(intday)) {
    err = 2;
    return false;
  }
  intMonth = parseInt(strMonth, 10);
  if (isNaN(intMonth)) {
    for (i = 0;i<12;i++)  
  {
  if (strMonth.toUpperCase() == strMonthArray[i].toUpperCase()) {
    intMonth = i+1;
    strMonth = strMonthArray[i];
    i = 12;
  }
}
  if (isNaN(intMonth)) {
    err = 3;
    return false;
  }
}
  intYear = parseInt(strYear, 10);
  if (isNaN(intYear)) {
    err = 4;
    return false;
  }
  if (intMonth>12 || intMonth<1) {
    err = 5;
    return false;
  }
  if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12) && (intday > 31 || intday < 1)) {
    err = 6;
    return false;
  }
  if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && (intday > 30 || intday < 1)) {
    err = 7;
    return false;
  }
  if (intMonth == 2) {
    if (intday < 1) {
      err = 8;
      return false;
  }
 if (LeapYear(intYear) == true) {
   if (intday > 29) {
     err = 9;
     return false;
   }
 }else {
   if (intday > 28) {
     err = 10;
     return false;
   }
 }
}
  if (strDatestyle == "US") {
    datefield.value = strMonthArray[intMonth-1] + " " + intday+" " + strYear;
  }else {
    datefield.value = intday + " " + strMonthArray[intMonth-1] + " " + strYear;
  }
  return true;
}*/

//Leap year detect function
function LeapYear(intYear) {
 if (intYear != null || intYear != " "){
  if (intYear % 100 == 0) {
    if (intYear % 400 == 0) { 
      return true; 
    }
  }else {
    if ((intYear % 4) == 0) { 
      return true; 
    }
  }
 }
return false;

}

// Checks to see if dates are valid when button is submitted 
function doDateCheck(from, to) {
 if (Date.parse(from.value) <= Date.parse(to.value)) {
  alert("The dates are valid.");
 }
 else 
 {
  if (from.value == "" || to.value == "") 
   alert("Both dates must be entered.");
  else 
   alert("To date must occur after the from date.");
 }
}

//Test for an email list - if one is bad the whole list is bad 
function isEMailList(str,del){
  var ans = new Array();
  var arys = new Array();
  arys = split(str,del);
  for(i=0; i < arys.length; i++){
    ans = (arys[i]);
  }
  if (isEmail(ans)== true){
    return true;
  }else {
    return false; 
  }
}

function isEmail (str) {   
  if (isEmpty(str)) 
  if (isEmail.arguments.length == 1) 
    return false;
  else 
    return (isEmail.arguments[1] == true);
  // is s whitespace?
  if (isWhitespace(str))
    return false;
  // there must be >= 1 character before @, so we
  // start looking at character position 1 
  // (i.e. second character)
  var i = 1;
  var sLength = str.length;
  // look for @
  while ((i < sLength) && (str.charAt(i) != "@")) {
    i++
  }
  if ((i >= sLength) || (str.charAt(i) != "@")) 
    //print('false because of @ missing');
    return false;
  else i += 2;
  // look for .
  while ((i < sLength) && (str.charAt(i) != ".")){
    i++
  }
  // there must be at least one character after the .
  if ((i >= sLength - 1) || (str.charAt(i) != ".")) 
    return false;
  else 
    return true;
}



////************************************************////
/////////////String manipulation Functions/////////////



// Attempting to make this library run on Navigator 2.0,
// so I'm supplying this array creation routine as per
// JavaScript 1.0 documentation.  If you're using 
// Navigator 3.0 or later, you don't need to do this;
// you can use the Array constructor instead.
function makeArray(n) {
  for (var i = 1; i <= n; i++) {
    this[i] = 0
  } 
  return this
}

// Removes all characters which appear in string bag from string str.
function stripCharsInBag (str, bag) {
  var i;
  var returnString = "";
  // Search through string's characters one by one.
  // If character is not in bag, append to returnString.
  for (i = 0; i < str.length; i++) {   
    // Check that current character isn't whitespace.
    var c = str.charAt(i);
    if (bag.indexOf(c) == -1) returnString += c;
  }
  return returnString;
}

// Removes all characters which do NOT appear in string bag 
// from string str.
function stripCharsNotInBag (str, bag) {
  var i;
  var returnString = "";
  // Search through string's characters one by one.
  // If character is in bag, append to returnString.
  for (i = 0; i < str.length; i++) {   
    // Check that current character isn't whitespace.
    var c = str.charAt(i);
    if (bag.indexOf(c) != -1) returnString += c;
  }
  return returnString;
}

// Removes all whitespace characters from str.
// Global variable whitespace (see above)
// defines which characters are considered whitespace.
function stripWhitespace (str) {
  return stripCharsInBag (str, whitespace)
}

// Removes initial (leading) whitespace characters from str.
// Global variable whitespace (see above)
// defines which characters are considered whitespace.
function stripInitialWhitespace (str) {
  var i = 0;
  while ((i < str.length) && charInString (str.charAt(i), whitespace))
    i++;
    return str.substring (i, str.length);
}

// reformat (TARGETSTRING, STRING, INTEGER, STRING, INTEGER ... )       
//
// Handy function for arbitrarily inserting formatting characters
// or delimiters of various kinds within TARGETSTRING.
//
// reformat takes one named argument, a string s, and any number
// of other arguments.  The other arguments must be integers or
// strings.  These other arguments specify how string s is to be
// reformatted and how and where other strings are to be inserted
// into it.
//
// reformat processes the other arguments in order one by one.
// * If the argument is an integer, reformat appends that number 
//   of sequential characters from s to the resultString.
// * If the argument is a string, reformat appends the string
//   to the resultString.
//
// NOTE: The first argument after TARGETSTRING must be a string.
// (It can be empty.)  The second argument must be an integer.
// Thereafter, integers and strings must alternate.  This is to
// provide backward compatibility to Navigator 2.0.2 JavaScript
// by avoiding use of the typeof operator.
//
// It is the caller's responsibility to make sure that we do not
// try to copy more characters from s than str.length.
//
// EXAMPLES:
//
// * To reformat a 10-digit U.s. phone number from "1234567890"
//   to "(123) 456-7890" make this function call:
//   reformat("1234567890", "(", 3, ") ", 3, "-", 4)
//
// * To reformat a 9-digit U.s. Social Security number from
//   "123456789" to "123-45-6789" make this function call:
//   reformat("123456789", "", 3, "-", 2, "-", 4)
//
// HINT:
//
// If you have a string which is already delimited in one way
// (example: a phone number delimited with spaces as "123 456 7890")
// and you want to delimit it in another way using function reformat,
// call function stripCharsNotInBag to remove the unwanted 
// characters, THEN call function reformat to delimit as desired.
//
// EXAMPLE:
//
// reformat (stripCharsNotInBag ("123 456 7890", digits),
//           "(", 3, ") ", 3, "-", 4)

function reformat (str) {
  var arg;
  var sPos = 0;
  var resultString = "";
  for (var i = 1; i < reformat.arguments.length; i++) {
    arg = reformat.arguments[i];
    if (i % 2 == 1) resultString += arg;
    else {
      resultString += str.substring(sPos, sPos + arg);
      sPos += arg;
    }
  }
  return resultString;
}

// daysInFebruary (INTEGER year)
// 
// Given integer argument year,
// returns number of days in February of that year.
function daysInFebruary (year) {
  // February has 29 days in any year evenly divisible by four,
  // EXCEPT for centurial years which are not also divisible by 400.
  return (  ((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0) ) ) ? 29 : 28 );
}

// Display prompt string s in status bar.
function prompt (str) {
   window.status = str
}

var pEntryPrompt = "Please enter a "

// Display data entry prompt string s in status bar.
function promptEntry (str) {
  window.status = pEntryPrompt + str
}

// takes ZIPString, a string of 5 or 9 digits;
// if 9 digits, inserts separator hyphen
function reformatZIPCode (ZIPString) {
  if (ZIPString.length == 5) return ZIPString;
  else return (reformat (ZIPString, "", 5, "-", 4));
}

// takes USPhone, a string of 10 digits
// and reformats as (123) 456-789
function reformatUSPhone (USPhone) {
  return (reformat (USPhone, "(", 3, ") ", 3, "-", 4))
}

// takes SSN, a string of 9 digits
// and reformats as 123-45-6789
function reformatSSN (SSN) {
  return (reformat (SSN, "", 3, "-", 2, "-", 4))
}

//Function to split a string into parts designated by the delimiter
function split(str, delimiter){
  var ary = new Array();
  var count = 0;
  var lastPos = -1;
  var curPos = 0;
  //move throught the string spliting each element out and place into array  
  while (curPos != lastPos){
    //reset (allows us to enter loop for first time)
    if(lastPos==-1){
      //reset value of lastPos
      lastPos=0;
    }
    curPos = str.indexOf(delimiter, curPos+1);
    //set the element after the last delimiter and return the array
    if(curPos < 0){
      // ary[count] get set to the values of str.substring
      ary[count] = str.substring(lastPos, str.length );
      return ary;
    }
    //continue if the curPos is not greater than the length of the string
    if(curPos <= str.length){
      ary[count] = str.substring(lastPos , curPos);
      //increment counts
      count += 1;
      lastPos = curPos + 1 ;
      //curPos += 1;
    }  
  } 
  return ary;     
}


   
// Get checked value from radio button.

function getRadioButtonValue (radio) {   
  for (var i = 0; i < radio.length; i++) {
    if (radio[i].checked) { 
      break 
    }
  }
    return radio[i].value
}

  
  
/////////////Credit Card Functions/////////////////////////

// non-digit characters which are allowed in credit card numbers
var creditCardDelimiters = "-"

// Validate credit card info.

function checkCreditCard (radio, theField)
{   //alert(theField);
    var cardType = getRadioButtonValue (radio)
    var creditCardDelimiters = "-"
    //alert(cardType);
   
    var normalizedCCN = stripCharsInBag(theField.value, creditCardDelimiters)
    // alert(normalizedCCN);
    if (!isCardMatch(cardType, normalizedCCN)) 
       return false;
    else 
    {  theField.value = normalizedCCN
       return true
    }
}



/*  ================================================================
    Credit card verification functions
    Originally included as Starter Application 1.0.0 in LivePayment.
    20 Feb 1997 modified by egk:
           changed naming convention to initial lowercase
                  (isMasterCard instead of IsMasterCard, etc.)
           changed isCC to isCreditCard
           retained functions named with older conventions from
                  LivePayment as stub functions for backward 
                  compatibility only
           added "AMERICANEXPRESS" as equivalent of "AMEX" 
                  for naming consistency 
    ================================================================ */


/*  ================================================================
    FUNCTION:  isCreditCard(st)
 
    INPUT:     st - a string representing a credit card number

    RETURNS:  true, if the credit card number passes the Luhn Mod-10
		    test.
	      false, otherwise
    ================================================================ */

function isCreditCard(st) {
  // Encoding only works on cards with less than 19 digits
  if (st.length > 19 || st.length < 10 )
    return (false);

  sum = 0; mul = 1; l = st.length;
  for (i = 0; i < l; i++) {
    digit = st.substring(l-i-1,l-i);
    tproduct = parseInt(digit ,10)*mul;
    if (tproduct >= 10)
      sum += (tproduct % 10) + 1;
    else
      sum += tproduct;
    if (mul == 1)
      mul++;
    else
      mul--;
  }
// Uncomment the following line to help create credit card numbers
// 1. Create a dummy number with a 0 as the last digit
// 2. Examine the sum written out
// 3. Replace the last digit with the difference between the sum and
//    the next multiple of 10.

  //document.writeln("<BR>Sum      = ",sum,"<BR>");
  //alert("Sum      = " + sum);

  if ((sum % 10) == 0)
    return (true);
  else
    return (false);

} // END FUNCTION isCreditCard()



/*  ================================================================
    FUNCTION:  isVisa()
 
    INPUT:     cc - a string representing a credit card number

    RETURNS:  true, if the credit card number is a valid VISA number.
		    
	      false, otherwise

    Sample number: 4111 1111 1111 1111 (16 digits)
    ================================================================ */

function isVisa(cc)
{
  if (((cc.length == 16) || (cc.length == 13)) &&
      (cc.substring(0,1) == 4))
    return isCreditCard(cc);
  return false;
}  // END FUNCTION isVisa()




/*  ================================================================
    FUNCTION:  isMasterCard()
 
    INPUT:     cc - a string representing a credit card number

    RETURNS:  true, if the credit card number is a valid MasterCard
		    number.
		    
	      false, otherwise

    Sample number: 5500 0000 0000 0004 (16 digits)
    ================================================================ */

function isMasterCard(cc)
{
  firstdig = cc.substring(0,1);
  seconddig = cc.substring(1,2);
  if ((cc.length == 16) && (firstdig == 5) &&
      ((seconddig >= 1) && (seconddig <= 5)))
    return isCreditCard(cc);
  return false;

} // END FUNCTION isMasterCard()





/*  ================================================================
    FUNCTION:  isAmericanExpress()
 
    INPUT:     cc - a string representing a credit card number

    RETURNS:  true, if the credit card number is a valid American
		    Express number.
		    
	      false, otherwise

    Sample number: 340000000000009 (15 digits)
    ================================================================ */

function isAmericanExpress(cc)
{
  firstdig = cc.substring(0,1);
  seconddig = cc.substring(1,2);
  if ((cc.length == 15) && (firstdig == 3) &&
      ((seconddig == 4) || (seconddig == 7)))
    return isCreditCard(cc);
  return false;

} // END FUNCTION isAmericanExpress()




/*  ================================================================
    FUNCTION:  isDinersClub()
 
    INPUT:     cc - a string representing a credit card number

    RETURNS:  true, if the credit card number is a valid Diner's
		    Club number.
		    
	      false, otherwise

    Sample number: 30000000000004 (14 digits)
    ================================================================ */

function isDinersClub(cc)
{
  firstdig = cc.substring(0,1);
  seconddig = cc.substring(1,2);
  if ((cc.length == 14) && (firstdig == 3) &&
      ((seconddig == 0) || (seconddig == 6) || (seconddig == 8)))
    return isCreditCard(cc);
  return false;
}



/*  ================================================================
    FUNCTION:  isCarteBlanche()
 
    INPUT:     cc - a string representing a credit card number

    RETURNS:  true, if the credit card number is a valid Carte
		    Blanche number.
		    
	      false, otherwise
    ================================================================ */

function isCarteBlanche(cc)
{
  return isDinersClub(cc);
}




/*  ================================================================
    FUNCTION:  isDiscover()
 
    INPUT:     cc - a string representing a credit card number

    RETURNS:  true, if the credit card number is a valid Discover
		    card number.
		    
	      false, otherwise

    Sample number: 6011000000000004 (16 digits)
    ================================================================ */

function isDiscover(cc)
{
  first4digs = cc.substring(0,4);
  if ((cc.length == 16) && (first4digs == "6011"))
    return isCreditCard(cc);
  return false;

} // END FUNCTION isDiscover()





/*  ================================================================
    FUNCTION:  isEnRoute()
 
    INPUT:     cc - a string representing a credit card number

    RETURNS:  true, if the credit card number is a valid enRoute
		    card number.
		    
	      false, otherwise

    Sample number: 201400000000009 (15 digits)
    ================================================================ */

function isEnRoute(cc)
{
  first4digs = cc.substring(0,4);
  if ((cc.length == 15) &&
      ((first4digs == "2014") ||
       (first4digs == "2149")))
    return isCreditCard(cc);
  return false;
}



/*  ================================================================
    FUNCTION:  isJCB()
 
    INPUT:     cc - a string representing a credit card number

    RETURNS:  true, if the credit card number is a valid JCB
		    card number.
		    
	      false, otherwise
    ================================================================ */

function isJCB(cc)
{
  first4digs = cc.substring(0,4);
  if ((cc.length == 16) &&
      ((first4digs == "3088") ||
       (first4digs == "3096") ||
       (first4digs == "3112") ||
       (first4digs == "3158") ||
       (first4digs == "3337") ||
       (first4digs == "3528")))
    return isCreditCard(cc);
  return false;

} // END FUNCTION isJCB()



/*  ================================================================
    FUNCTION:  isAnyCard()
 
    INPUT:     cc - a string representing a credit card number

    RETURNS:  true, if the credit card number is any valid credit
		    card number for any of the accepted card types.
		    
	      false, otherwise
    ================================================================ */

function isAnyCard(cc)
{
  if (!isCreditCard(cc))
    return false;
  if (!isMasterCard(cc) && !isVisa(cc) && !isAmericanExpress(cc) && !isDinersClub(cc) &&
      !isDiscover(cc) && !isEnRoute(cc) && !isJCB(cc)) {
    return false;
  }
  return true;

} // END FUNCTION isAnyCard()



/*  ================================================================
    FUNCTION:  isCardMatch()
 
    INPUT:    cardType - a string representing the credit card type
	      cardNumber - a string representing a credit card number

    RETURNS:  true, if the credit card number is valid for the particular
	      credit card type given in "cardType".
		    
	      false, otherwise
    ================================================================ */

function isCardMatch (cardType, cardNumber)
{

	cardType = cardType.toUpperCase();
	var doesMatch = true;

	if ((cardType == "VISA") && (!isVisa(cardNumber)))
		doesMatch = false;
	if ((cardType == "MASTERCARD") && (!isMasterCard(cardNumber)))
		doesMatch = false;
	if ( ( (cardType == "AMERICANEXPRESS") || (cardType == "AMEX") )
                && (!isAmericanExpress(cardNumber))) doesMatch = false;
	if ((cardType == "DISCOVER") && (!isDiscover(cardNumber)))
		doesMatch = false;
	if ((cardType == "JCB") && (!isJCB(cardNumber)))
		doesMatch = false;
	if ((cardType == "DINERS") && (!isDinersClub(cardNumber)))
		doesMatch = false;
	if ((cardType == "CARTEBLANCHE") && (!isCarteBlanche(cardNumber)))
		doesMatch = false;
	if ((cardType == "ENROUTE") && (!isEnRoute(cardNumber)))
		doesMatch = false;
	return doesMatch;

}  // END FUNCTION CardMatch()




/*  ================================================================
    The below stub functions are retained for backward compatibility
    with the original LivePayment code so that it should be possible
    in principle to swap in this new module as a replacement for the  
    older module without breaking existing code.  (There are no
    guarantees, of course, but it should work.)

    When writing new code, do not use these stub functions; use the
    functions defined above.
    ================================================================ */

function IsCC (st) {
    return isCreditCard(st);
}

function IsVisa (cc)  {
  return isVisa(cc);
}

function IsVISA (cc)  {
  return isVisa(cc);
}

function IsMasterCard (cc)  {
  return isMasterCard(cc);
}

function IsMastercard (cc)  {
  return isMasterCard(cc);
}

function IsMC (cc)  {
  return isMasterCard(cc);
}

function IsAmericanExpress (cc)  {
  return isAmericanExpress(cc);
}

function IsAmEx (cc)  {
  return isAmericanExpress(cc);
}

function IsDinersClub (cc)  {
  return isDinersClub(cc);
}

function IsDC (cc)  {
  return isDinersClub(cc);
}

function IsDiners (cc)  {
  return isDinersClub(cc);
}

function IsCarteBlanche (cc)  {
  return isCarteBlanche(cc);
}

function IsCB (cc)  {
  return isCarteBlanche(cc);
}

function IsDiscover (cc)  {
  return isDiscover(cc);
}

function IsEnRoute (cc)  {
  return isEnRoute(cc);
}

function IsenRoute (cc)  {
  return isEnRoute(cc);
}

function IsJCB (cc)  {
  return isJCB(cc);
}

function IsAnyCard(cc)  {
  return isAnyCard(cc);
}

function IsCardMatch (cardType, cardNumber)  {
  return isCardMatch (cardType, cardNumber);
}


