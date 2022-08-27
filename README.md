# lite-decimal

Decimal in C#/.NET is a awesome data type for storing and computing in finanacial solution. Decimal is 16 bytes and has range of -79,228,162,514,264,337,593,543,950,335 to +79,228,162,514,264,337,593,543,950,335. Many of the scenarios does not require as much Range and digits after decimal point as decimal data type supports. 

lite-decimal is a data type similar to decimal but it will require 8 bytes and will support range of -576,460,752,303,423,487 to +576,460,752,303,423,487 and it could support upto 15 digits after decimal places.

## support
* Operator overloading
    * addition
    * substraction
    * multiplication
    * division
* Type casting
    * implicit
        * int
        * short
    * explicit
        * long
        * float
        * double
        * decimal
* Methods
    * Get Bytes
    * From Bytes
    * Parse / TryParse

## Internals
* Uses long to store both value and decimal point position.
* 4 bits (upto 15 digits after decimal) to store decimal point position.
* 59 bits for storing value
* 1 bit for sign. 1 for negative, 0 for positive.


## Goal
* Light weight data type similar to decimal.
* High performance