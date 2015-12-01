typedef unsigned char byte;
typedef unsigned short ushort;

bool UnCompress(byte* source, byte* target)
{
    if (*source++ != 0x10)
        return false;

    int positionUncomp = 0;
    int lenght = *(source++) + (*(source++) << 8) + (*(source++) << 16);

    while (positionUncomp < lenght)
    {
        byte isCompressed = *(source++);
        for (int i = 0; i < 8; i++)
        {
            if ((isCompressed & 0x80) != 0)
            {
                byte amountToCopy = 3 + (*(source) >> 4);
                ushort copyPosition = 1;
                copyPosition += (*(source++) & 0xF) << 8;
                copyPosition += *(source++);

                if (copyPosition > lenght)
                    return false;

                for (int u = 0; u < amountToCopy; u++)
                {
                    *(target + positionUncomp) = *((target + positionUncomp - u) - copyPosition + (u % copyPosition));
                    positionUncomp++;
                }
            }
            else
            {
                *(target + positionUncomp++) = *(source++);
            }
            if (!(positionUncomp < lenght))
                break;

            isCompressed <<= 1;
        }
    }
    return true;
}