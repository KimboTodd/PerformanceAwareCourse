// See https://aka.ms/new-console-template for more information

using System.Collections;

var isSet = (byte b, int bitNumber) => (b & (1 << bitNumber)) != 0;

var file = "/Users/kimtodd/repos/computer_enhance/perfaware/part1/listing_0037_single_register_mov";
// Console.WriteLine(args[0]);

var filestream = File.Open(file, FileMode.Open);
using var reader = new BinaryReader(filestream);
using var writer = new StreamWriter($"{Path.GetFileNameWithoutExtension(file)}.asm");
writer.WriteLine("bits 16");
writer.WriteLine("");

while (reader.BaseStream.Position != reader.BaseStream.Length)
{
    var instruction = string.Empty;
    var destination = string.Empty;
    var source = string.Empty;

    var instructionBytes = reader.ReadByte();
    var bits = new BitArray(new[] { instructionBytes });

    instruction = bits switch
    {
        [_, _, false, true, false, false, false, true] => "mov", // MOV 100010
        _ => throw new Exception(),
    };

    var secondItem = reader.ReadByte();
    var secondBits = new BitArray(new[] { secondItem });

    // mod 2 bits, reg 3 bits, r/m 3 bits
    if (secondBits is [.., true, true])
    {
        Console.WriteLine("yep");
    }

    var wBit = bits[0];
    // d bit determines which field is destination bits[1]

    if (bits[1])
    {
        destination = wBit
            ? secondBits switch
            {
                [_, _, _, false, false, false, _, _] => "AX",
                [_, _, _, true, false, false, _, _] => "CX",
                [_, _, _, false, true, false, _, _] => "DX",
                [_, _, _, true, true, false, _, _] => "BX",
                [_, _, _, false, false, true, _, _] => "SP",
                [_, _, _, true, false, true, _, _] => "BP",
                [_, _, _, false, true, true, _, _] => "SI",
                [_, _, _, true, true, true, _, _] => "DI",
                _ => throw new ArgumentOutOfRangeException(),
            }
            : secondBits switch
            {
                [_, _, _, false, false, false, _, _] => "AL",
                [_, _, _, true, false, false, _, _] => "CL",
                [_, _, _, false, true, false, _, _] => "DL",
                [_, _, _, true, true, false, _, _] => "BL",
                [_, _, _, false, false, true, _, _] => "AH",
                [_, _, _, true, false, true, _, _] => "CH",
                [_, _, _, false, true, true, _, _] => "DH",
                [_, _, _, true, true, true, _, _] => "BH",
                _ => throw new ArgumentOutOfRangeException(),
            };

        source = wBit
            ? secondBits switch
            {
                [false, false, false, ..] => "AX",
                [true, false, false, ..] => "CX",
                [false, true, false, ..] => "DX",
                [true, true, false, ..] => "BX",
                [false, false, true, ..] => "SP",
                [true, false, true, ..] => "BP",
                [false, true, true, ..] => "SI",
                [true, true, true, ..] => "DI",
                _ => throw new ArgumentOutOfRangeException(),
            }
            : secondBits switch
            {
                [false, false, false, ..] => "AL",
                [true, false, false, ..] => "CL",
                [false, true, false, ..] => "DL",
                [true, true, false, ..] => "BL",
                [false, false, true, ..] => "AH",
                [true, false, true, ..] => "CH",
                [false, true, true, ..] => "DH",
                [true, true, true, ..] => "BH",
                _ => throw new ArgumentOutOfRangeException(),
            };
    }
    else if (bits[1] == false)
    {
        source = wBit
            ? secondBits switch
            {
                [_, _, _, false, false, false, _, _] => "AX",
                [_, _, _, true, false, false, _, _] => "CX",
                [_, _, _, false, true, false, _, _] => "DX",
                [_, _, _, true, true, false, _, _] => "BX",
                [_, _, _, false, false, true, _, _] => "SP",
                [_, _, _, true, false, true, _, _] => "BP",
                [_, _, _, false, true, true, _, _] => "SI",
                [_, _, _, true, true, true, _, _] => "DI",
                _ => throw new ArgumentOutOfRangeException(),
            }
            : secondBits switch
            {
                [_, _, _, false, false, false, _, _] => "AL",
                [_, _, _, true, false, false, _, _] => "CL",
                [_, _, _, false, true, false, _, _] => "DL",
                [_, _, _, true, true, false, _, _] => "BL",
                [_, _, _, false, false, true, _, _] => "AH",
                [_, _, _, true, false, true, _, _] => "CH",
                [_, _, _, false, true, true, _, _] => "DH",
                [_, _, _, true, true, true, _, _] => "BH",
                _ => throw new ArgumentOutOfRangeException(),
            };

        destination = wBit
            ? secondBits switch
            {
                [false, false, false, ..] => "AX",
                [true, false, false, ..] => "CX",
                [false, true, false, ..] => "DX",
                [true, true, false, ..] => "BX",
                [false, false, true, ..] => "SP",
                [true, false, true, ..] => "BP",
                [false, true, true, ..] => "SI",
                [true, true, true, ..] => "DI",
                _ => throw new ArgumentOutOfRangeException(),
            }
            : secondBits switch
            {
                [false, false, false, ..] => "AL",
                [true, false, false, ..] => "CL",
                [false, true, false, ..] => "DL",
                [true, true, false, ..] => "BL",
                [false, false, true, ..] => "AH",
                [true, false, true, ..] => "CH",
                [false, true, true, ..] => "DH",
                [true, true, true, ..] => "BH",
                _ => throw new ArgumentOutOfRangeException(),
            };
    }
    writer.WriteLine($"{instruction} {destination} {source}");
}


// save into a .asm file

// check it manually