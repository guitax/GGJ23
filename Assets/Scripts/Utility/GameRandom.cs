using System;
using System.Collections.Generic;
using System.Linq;

public static class GameRandom
{
    public static IRandom Core { get; set; }
    //public static IRandom View { get; set; }
}

public interface IRandom
{
    int NextInt();
    int NextInt(int max);
    int NextInt(int min, int max);
    int NextSign();
    float NextFloat();
    float NextFloat(float max);
    float NextFloat(float min, float max);
    T NextElement<T>(IEnumerable<T> enumerable);
    IEnumerable<T> Shuffle<T>(IEnumerable<T> enumerable);
    int NextEnum(Type enumType);
    IEnumerable<int> ShuffleEnum(Type enumType);
}

public class DefaultRandom : IRandom
{
    private readonly Random _random;

    public DefaultRandom()
    {
        _random = new Random();
    }

    public DefaultRandom(int seed)
    {
        _random = new Random(seed);
    }

    public int NextInt()
    {
        return _random.Next();
    }

    public int NextInt(int max)
    {
        return _random.Next(max);
    }

    public int NextInt(int min, int max)
    {
        return _random.Next(min, max);
    }

    public int NextSign()
    {
        return NextInt(2) * 2 - 1;
    }

    public float NextFloat()
    {
        return (float)_random.NextDouble();
    }

    public float NextFloat(float max)
    {
        if (max < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(max), max, $"'{nameof(max)}' must be greater than zero.");
        }

        return NextFloat(0, max);
    }

    public float NextFloat(float min, float max)
    {
        if (min > max)
        {
            throw new ArgumentOutOfRangeException($"{nameof(min)} | {nameof(max)}", $"{min} | {max}",
                $"'{nameof(min)}' cannot be greater than '{nameof(max)}'.");
        }

        return (float)(min + (((double)max - min) * _random.NextDouble()));
    }

    public T NextElement<T>(IEnumerable<T> enumerable)
    {
        if (enumerable is null)
        {
            throw new ArgumentNullException(nameof(enumerable), $"'{nameof(enumerable)}' cannot be null.");
        }

        int count = enumerable.Count();
        if (count == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(enumerable), $"'{nameof(enumerable)}' cannot be empty.");
        }

        return enumerable.ElementAt(NextInt(count));
    }

    public IEnumerable<T> Shuffle<T>(IEnumerable<T> enumerable)
    {
        if (enumerable is null)
        {
            throw new ArgumentNullException(nameof(enumerable));
        }

        T[] array = enumerable.ToArray();
        if (array.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(enumerable), $"'{nameof(enumerable)}' cannot be empty.");
        }

        return ShuffleIterator();

        IEnumerable<T> ShuffleIterator()
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                // ... except we don't really need to swap it fully, as we can
                // return it immediately, and afterwards it's irrelevant.
                int swapIndex = NextInt(i + 1);
                yield return array[swapIndex];
                array[swapIndex] = array[i];
            }
        }
    }

    public int NextEnum(Type enumType)
    {
        IEnumerable<int> enumValues = Enum.GetValues(enumType).OfType<int>();
        if (!enumValues.Any())
        {
            throw new ArgumentOutOfRangeException(nameof(enumType), $"Enum of type '{nameof(enumValues)}' cannot have no values.");
        }

        return NextElement(enumValues);
    }

    public IEnumerable<int> ShuffleEnum(Type enumType)
    {
        int[] enumValues = Enum.GetValues(enumType) as int[];
        if (enumValues.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(enumValues), $"Enum of type '{nameof(enumValues)}' cannot have no values.");
        }

        return Shuffle(enumValues);
    }
}
