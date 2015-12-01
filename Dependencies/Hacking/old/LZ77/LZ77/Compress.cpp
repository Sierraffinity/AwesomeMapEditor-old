#include <vector>

using std::vector;

typedef unsigned char byte;
typedef unsigned short ushort;

byte[] Compress(byte* source, int lenght)
{
    int position = 0;

    List<byte> CompressedData = new List<byte>();
    CompressedData.Add(0x10);

    {
        byte* pointer = (byte*)&lenght;
        for (int i = 0; i < 3; i++)
        {
            CompressedData.Add(*(pointer++));
        }
    }

    while (position < lenght)
    {
        byte isCompressed = 0;
        List<byte> tempList = new List<byte>();

        for (int i = 0; i < BlockSize; i++)
        {
            int[] searchResult = Search(source, position, lenght);

            if (searchResult[0] > 2)
            {
                byte add = (byte)((((searchResult[0] - 3) & 0xF) << 4) + (((searchResult[1] - 1) >> 8) & 0xF));
                tempList.Add(add);
                add = (byte)((searchResult[1] - 1) & 0xFF);
                tempList.Add(add);
                position += searchResult[0];
                isCompressed |= (byte)(1 << (8 - i - 1));
            }
            else if (searchResult[0] >= 0)
                tempList.Add(*(source + position++));
            else
                break;
        }
        CompressedData.Add(isCompressed);
        CompressedData.AddRange(tempList);
    }
    while (CompressedData.Count % 4 != 0)
        CompressedData.Add(0);

    return CompressedData.ToArray();
}

int[] Search(byte* source, int position, int lenght)
{
    vector<int> results;

    if ((position < 3) || ((lenght - position) < 3))
        return new int[2] { 0, 0 };
    if (!(position < lenght))
        return new int[2] { -1, 0 };

    for (int i = 1; ((i < 0xfff + 1) && (i < position)); i++)
    {
        if (*(source + position - i - 1) == *(source + position))
        {
			results.push_back(i + 1);
        }
    }
	if (results.size == 0)
        return new int[2] { 0, 0 };

    int amountOfBytes = 0;

    while (amountOfBytes < 0xf + 3)
    {
        amountOfBytes++;
        bool Break = false;
        for (int i = 0; i < results.size; i++)
        {
            if (*(source + position + amountOfBytes) != *(source + position - results[i] + (amountOfBytes % (results[i]))))
            {
                if (results.size > 1)
                {
					results.erase(i);
                    i--;
                }
                else
                    goto end;
            }
        }
    }
end:
    return new int[2] { amountOfBytes, results[0] }; //lenght of data is first, then position
}
