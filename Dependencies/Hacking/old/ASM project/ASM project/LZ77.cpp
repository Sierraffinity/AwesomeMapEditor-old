typedef unsigned __int32 u32;
typedef unsigned __int16 u16;
typedef unsigned __int8 byte;

bool LZ77uncompress(u32* source, u32* destination)
{
	//esi = remaining data to uncompress
	//ebx = compressed data pointer
	//edi = uncompressed data pointer
	u32 lenght;
	__asm
	{
		mov eax, [source]
		cmp al, 0x10
		jne fail

		mov ebx, source
		mov edi, destination
		shr eax, 8
		mov esi, eax
		mov lenght, esi
		add ebx, 4
	}
	byte isCompressed;
compTest:
	__asm
	{
		mov al, [ebx]
		mov isCompressed, al
		inc ebx
	}
	__asm
	{
		mov al, isCompressed
		bt al, 0x8
		jnc notCompressed
	}









compressed:
	__asm
	{

		mov cx, [ebx]
		mov dx, cx
		shr cx, 8
		and cx, 0xf
		shr dh, 8 //dx = position, cx = amount to copy //(target + positionUncomp) = *((target + positionUncomp - u) - copyPosition + (u % copyPosition));
loop:
		

	}

copyStraight:
	__asm
	{
		pop eax, edx, ecx
		push ebx

		mov ebx, eax //ebx = pointer to source, ecx = amount to copy, edx = pointer to destination
copyLoop:
		mov eax, [ebx]
		mov [edx], eax
		inc ebx
		inc edx
		dec ecx
		jnz copyLoop

		pop ebx
		ret
	}

/*notCompressed:
	__asm
	{
		
		mov al, [ebx]
		mov [edi], al
		inc ebx
		inc edi		
		dec esi
		
	}*/

	return true;
fail:
	return false;
}


bool UnCompress(byte* source, byte* target)
{
    if (*source++ != 0x10)
        return false;

    u32 positionUncomp = 0;
    u32 lenght = *(source++) + (*(source++) << 8) + (*(source++) << 16);

    while (positionUncomp < lenght)
    {
        byte isCompressed = *(source++);
        for (u32 i = 0; i < 8; i++)
        {
            if ((isCompressed & 0x80) != 0)
            {
                u32 amountToCopy = 3 + (*(source) >> 4);
                u32 copyPosition = 1;
                copyPosition += (*(source++) & 0xF) << 8;
                copyPosition += *(source++);

                if (copyPosition > lenght)
                    return false;

                for (u32 u = 0; u < amountToCopy; u++)
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
