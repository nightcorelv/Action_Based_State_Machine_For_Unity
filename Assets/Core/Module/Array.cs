using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Array
    {

        //resize
        public static void ReSize<T>(ref T[] array, int newSize)
        {
            if (newSize > 0 && newSize >= array.Length)
            {
                T[] temp = new T[newSize];
                for (int i = 0; i < array.Length; i++)
                {
                    temp[i] = array[i];
                }
                array = temp;
            }
            else if (newSize > 0 && newSize < array.Length)
            {
                T[] temp = new T[newSize];
                for (int i = 0; i < newSize; i++)
                {
                    temp[i] = array[i];
                }
                array = temp;
            }
            else if (newSize == 0)
            {
                array = new T[0];
            }
        }

        //add
        public static void Add<T>(ref T[] array, ref int size, T value)
        {
            if (array.Length == 0 && size == 0)
            {
                array = new T[++size];
            }
            else if (array.Length == size && size > 0)
            {
                T[] temp = new T[size * 2];
                for (int i = 0; i < size; i++)
                {
                    temp[i] = array[i];
                }
                temp[size++] = value;
                array = temp;
            }
            else if (array.Length > size)
            {
                array[size++] = value;
            }
            else if (array.Length < size)
            {
                size = array.Length;
                T[] temp = new T[size * 2];
                for (int i = 0; i < size; i++)
                {
                    temp[i] = array[i];
                }
                temp[size++] = value;
                array = temp;
            }

        }
        public static void Add<T>(ref T[] array, T value)
        {
            T[] temp = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                temp[i] = array[i];
            }
            temp[array.Length] = value;
            array = temp;
        }

        //remove
        public static void RemoveAt<T>(ref T[] array, ref int size, int index)
        {
            if (size == index && array.Length > 0 && size > 0)
            {
                RemoveLastIndex(ref array, ref size);
            }
            else if (index <= size && array.Length > 0 && size > 0)
            {
                size--;
                for (int i = index; i < size; i++)
                {
                    array[i] = array[i + 1];
                }
                array[size] = default;
            }

        }
        public static void RemoveAt<T>(ref T[] array, int index)
        {
            if (array.Length >= index)
            {
                T[] temp = new T[array.Length - 1];
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i < index)
                    {
                        temp[i] = array[i];
                    }
                    else if (i >= index)
                    {
                        temp[i] = array[i + 1];
                    }
                }
                array = temp;
            }

        }
        public static void RemoveLastIndex<T>(ref T[] array, ref int size)
        {
            if (array.Length > 0 && size > 0)
            {
                size -= 1;
                array[size] = default;
            }

        }
        public static void RemoveLastIndex<T>(ref T[] array)
        {
            if (array.Length > 0)
            {
                T[] temp = new T[array.Length - 1];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = array[i];
                }
                array = temp;
            }

        }

        //insert
        public static void InsertAt<T>(ref T[] array, ref int size, int index, T value)
        {
            if (size > index && size <= array.Length)
            {
                if (size == array.Length)
                {
                    ReSize(ref array, size * 2);
                }
                for (int i = size++; i < index; i--)
                {
                    array[i] = array[i - 1];
                }
                array[index] = value;
            }
            else if (size == index && size <= array.Length)
            {
                if (size == array.Length)
                {
                    //size |= size >> 16;
                }
                Add(ref array, value);
            }
        }
        public static void InsertAt<T>(ref T[] array, int index, T value)
        {
            if (array.Length > index)
            {
                T[] temp = new T[array.Length + 1];
                for (int i = 0; i < array.Length; i++)
                {
                    if (i < index)
                    {
                        temp[i] = array[i];
                    }
                    else if (i == index)
                    {
                        temp[i] = value;
                        temp[i + 1] = array[i];
                    }
                    else if (i > index)
                    {
                        temp[i + 1] = array[i];
                    }
                }
                array = temp;
            }
            else if (array.Length == index)
            {
                Add(ref array, value);
            }
        }

        //convert to List
        public static List<T> ToList<T>(ref T[] array)
        {
            return new List<T>(array);
        }
    }
}
