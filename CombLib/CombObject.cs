using System.Collections.Concurrent;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CombLib
{
    public class CombObject<T>
    {
        List<T> alphabet;

        public CombObject()
        {
            this.alphabet = new List<T>();
        }

        public CombObject(List<T> alphabet){
            this.alphabet = alphabet;
        }
        public IEnumerable<HashSet<T>[]> GetUnNamedPartitions(int setCount, List<HashSet<T>> current = null, List<T> alphabet = null)
        {
            //Инициализация начальных данных
            if (alphabet == null) alphabet = new List<T>(this.alphabet);
            if (current == null) current = new List<HashSet<T>>();

            //Если использованы все элементы алфавита
            if (alphabet.Count == 0)
            {
                //Если количесвто подмножеств в разбиениии равно искомому количеству
                if (current.Count == setCount)
                {
                    //Добавление разбиения
                    yield return current.ToArray();
                }
                //Выход из итерации
                yield break;
            }

            //Елемент для добавления в разбиение
            T element = alphabet[0];

            //Если количество множеств в рассматриваемом разбиении меньше нужного
            if (current.Count < setCount)
            {
                //Рассматриваем все множества текущего разбиения 
                for (int i = 0; i < current.Count+1; i++)
                {
                    //Создаем копию разбиения
                    List<HashSet<T>> newPartition = new List<HashSet<T>>();
                    for (int j = 0; j < current.Count; j++) newPartition.Add(new HashSet<T>(current[j]));

                    //Рассматриваю существующие подмножества в разбиении
                    if (i < current.Count)
                    {
                        //Добавляю елемент в i-ое подмножество разбиения
                        newPartition[i].Add(element);
                        //Рекурсивно вызываю метод для получения всех оставшихся разбиений из оставшихся эелементов
                        foreach (HashSet<T>[] partition in GetUnNamedPartitions(setCount, newPartition, alphabet.Skip(1).ToList())){
                            yield return partition;
                        }
                    }
                    //Добавляю новое пожмножество с element в разбиение
                    else
                    {
                        newPartition.Add(new HashSet<T> { element });
                        //Рекурсивно вызываю метод для получения всех оставшихся разбиений из оставшихся эелементов
                        foreach (HashSet<T>[] partition in GetUnNamedPartitions(setCount, newPartition, alphabet.Skip(1).ToList())){
                            yield return partition;
                        }
                    }
                }

            }
            //Если количество подмножеств в расматриваемом разбиении равно нужному количеству
            else
            {
                //Рассматриваем все множества текущего разбиения 
                for (int i = 0; i < current.Count ; i++)
                {
                    //Создаем копию разбиения
                    List<HashSet<T>> newPartition = new List<HashSet<T>>();
                    for (int j = 0; j < current.Count; j++) newPartition.Add(new HashSet<T>(current[j]));
                    //Добавляю елемент в i-ое подмножество разбиения
                    newPartition[i].Add(element);
                    //Рекурсивно вызываю метод для получения всех оставшихся разбиений из оставшихся эелементов
                    foreach (HashSet<T>[] partition in GetUnNamedPartitions(setCount, newPartition, alphabet.Skip(1).ToList()))
                    {
                        yield return partition;
                    }
                }
            }
        }
        public IEnumerable<HashSet<T>[]> GetNamedPartitions(int setCount, List<T> alphabet = null)
        {
            //Вспомогательная функция для перестановки 2х элементов
            void Swap(HashSet<T>[] set, int a, int b)
            {
                HashSet<T> temp =set[a];
                set[a] = set[b];
                set[b] = temp;
            }

            //Проходимся по каждому неименованному разбиению
            foreach (HashSet<T>[] partitions in GetUnNamedPartitions(setCount, null, alphabet))
            {
                int[] countSwap = new int[setCount];
                yield return partitions;
                    
                for (int i = 0; i < setCount;)
                {
                    if (countSwap[i] < i)
                    {
                        // Если i четное, обмен первого элемента с текущим
                        // Если i нечетное, обмен текущего элемента с элементом countSwap[i]
                        int swapIndex = (i % 2 == 0) ? 0 : countSwap[i];
                        Swap(partitions, swapIndex, i);
                        yield return partitions;

                        // Увеличиваем индекс текущего элемента
                        countSwap[i]++;
                        i = 0; // Сброс индекса
                    }
                    else
                    {
                        // Если мы дошли до конца, сбрасываем индекс
                        countSwap[i] = 0;
                        i++; // Переход к следующему элементу
                    }
                }
            }
        }
        public IEnumerable<List<T>[]> GetUnNamedPartitionsWithOneElement(int setCount, int setSize, T element) 
        {
            //Содаю вспомогательный массив в котором будут хранится размеры подмножеств в разбиении
            int[] partitionSizes = new int[setCount];
            //Запалняю его начальными значениями когда все кроме последнего подмножества состоят из одного элемента
            for (int i = 0;i < setCount-1;i++)
            {
                partitionSizes[i] = 1;
            }
            //Последнее подмножества состоит из оставшихся
            partitionSizes[setCount-1] = setSize-(setCount-1);

            while (partitionSizes[0] <= (int)(setSize / setCount))
            {
                //Инициализирую новое разбиение
                List<T>[] partition = new List<T>[setCount];
                //Заполняю новое разбиение необходимым элементом;
                for (int setIndex = 0; setIndex < setCount; setIndex++)
                {
                    partition[setIndex] = new List<T>();
                    for (int i = 0; i < partitionSizes[setIndex]; i++) partition[setIndex].Add(element);
                }
                //Возвращаю новое разбиение
                yield return partition;

                //Устанавливаю указатель на предпоследний элемент
                int index = setCount - 2;
                //Если требуемое количество подмножеств в разбиении 0 или 1 завершаю алгоритм
                if (index < 0) yield break;

                //Изменяю размеры подмножеств в разбиении сохраняя общее число элементов
                partitionSizes[index] += 1;
                partitionSizes[index + 1] -= 1;

                while (index > 0 && partitionSizes[index] > partitionSizes[setCount-1])
                {
                    partitionSizes[--index] += 1;
                    int sum = 0;
                    for (int i = 0; i < setCount - 1; i++)
                    {
                        if (i > index) partitionSizes[i] = partitionSizes[index];
                        sum += partitionSizes[i];
                    }
                    partitionSizes[setCount - 1] = setSize - sum;
                    
                }
            }
        }
        public IEnumerable<List<T>[]> GetNamedPartitionsWithOneElement(int setCount, int setSize, T element)
        {
            //Содаю вспомогательный массив в котором будут хранится размеры подмножеств в разбиении
            int[] partitionSizes = new int[setCount];
            //Заполняю его начальными значениями когда все кроме последнего подмножества состоят из одного элемента
            for (int i = 0; i < setCount; i++)
            {
                partitionSizes[i] = 1;
            }

            while (partitionSizes[0] <= setSize)
            {
                //Устанавливаю указатель на последний элемент
                int index = setCount - 1;
                if (index < 0) yield break;

                //Расчитываю нынешнюю сумму
                int sum = 0;
                for (int i = 0; i < setCount - 1; i++)
                {
                    sum += partitionSizes[i];
                }

                if (setSize - sum > 0)
                {
                    List<T>[] partition = new List<T>[setCount];
                    //Заполняю новое разбиение необходимым элементом;
                    for (int setIndex = 0; setIndex < setCount; setIndex++)
                    {
                        partition[setIndex] = new List<T>();
                        for (int i = 0; i < partitionSizes[setIndex]; i++) partition[setIndex].Add(element);
                    }
                    //Возвращаю новое разбиение
                    yield return partition;
                    index--;
                }

                //проверяю на выполнение всех условий
                while (partitionSizes[index] > setSize || partitionSizes[index] < 1)
                {
                    partitionSizes[index] = 1;
                    index--;
                }
                //перевожу указатель на последний элемент
                partitionSizes[index]++;
            }
        }
        public IEnumerable<List<T>[]> GetAllCycles(int setCount, List<List<T>> current = null, List<T> alphabet = null)
        {
            //Инициализация начальных данных
            if (alphabet == null) alphabet = new List<T>(this.alphabet);
            if (current == null) current = new List<List<T>>();

            //Если использованы все элементы алфавита
            if (alphabet.Count == 0)
            {
                //Если количесвто подмножеств в разбиениии равно искомому количеству
                if (current.Count == setCount)
                {
                    //Добавление разбиения
                    yield return current.ToArray();
                }
                //Выход из итерации
                yield break;
            }

            //Елемент для добавления в разбиение
            T element = alphabet[0];

            //Если количество множеств в рассматриваемом разбиении меньше нужного
            if (current.Count < setCount)
            {
                //Рассматриваем все множества текущего разбиения 
                for (int i = 0; i < current.Count + 1; i++)
                {
                    List<List<T>> newPartition = new List<List<T>>();
                    for (int j = 0; j < current.Count; j++) newPartition.Add(new List<T>(current[j]));

                    if (i < current.Count)
                    {
                        int count = newPartition[i].Count;
                        for (int j = 0; j < count; j++)
                        {
                            if (!newPartition[i].Contains(element)) newPartition[i].Insert(j, element);
                            foreach (List<T>[] partition in GetAllCycles(setCount, newPartition, alphabet.Skip(1).ToList()))
                            {
                                yield return partition;
                            }
                        }
                    }
                    else
                    {
                        newPartition.Add(new List<T> { element });
                        //Рекурсивно вызываю метод для получения всех оставшихся разбиений из оставшихся эелементов
                        foreach (List<T>[] partition in GetAllCycles(setCount, newPartition, alphabet.Skip(1).ToList()))
                        {
                            yield return partition;
                        }
                    }
                }

            }
            //Если количество подмножеств в расматриваемом разбиении равно нужному количеству
            else
            {
                //Рассматриваем все множества текущего разбиения 
                for (int i = 0; i < current.Count; i++)
                {
                    List<List<T>> newPartition = new List<List<T>>();
                    for (int j = 0; j < current.Count; j++) newPartition.Add(new List<T>(current[j]));

                    int count = newPartition[i].Count;
                    for (int j = 0; j < count; j++)
                    {
                        if (!newPartition[i].Contains(element)) newPartition[i].Insert(j, element);

                        foreach (List<T>[] partition in GetAllCycles(setCount, newPartition, alphabet.Skip(1).ToList()))
                        {
                            yield return partition;
                        }
                    }
                }
            }
        }

        //Получение пути к файлу
        string GetPath(string name)
        {
            string? appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(appDir, name);
        }
        public void WriteToFile(string name, IEnumerable<List<T>[]> data)
        {
            string path = GetPath("out.txt");

            try
            {
                StreamWriter sw = new StreamWriter(path);
                foreach (List<T>[] partition in data)
                {
                    foreach (List<T> set in partition)
                    {
                        sw.Write("{");
                        for (int i = 0; i < set.Count; i++)
                        {
                            if (i == set.Count - 1)
                            {
                                sw.Write(set[i]);
                                continue;
                            }
                            sw.Write(set[i] + " ");
                        }
                        sw.Write("} ");
                    }
                    sw.Write("\n");
                }
                sw.Close();
            }
            catch (Exception ex) { Console.WriteLine($"Exeption: {ex.Message}"); }
        }
        public void WriteToFile(string name, IEnumerable<HashSet<T>[]> data)
        {
            string path = GetPath("out.txt");

            try
            {
                StreamWriter sw = new StreamWriter(path);
                foreach (HashSet<T>[] partition in data)
                {
                    foreach (HashSet<T> set in partition)
                    {
                        sw.Write("{ ");
                        
                        foreach (T element in set)
                        {
                            sw.Write(element + " ");
                        }
                        sw.Write("} ");
                    }
                    sw.Write("\n");
                }
                sw.Close();
            }
            catch (Exception ex) { Console.WriteLine($"Exeption: {ex.Message}"); }
        }

    }

    public class Comb
    {
        public Comb() { return; }
        public long CountOfCombinations(int n, int k)
        {
            if (k == n || k == 0) return 1;

            if (k > n) return CountOfCombinations(n + 1, k) - CountOfCombinations(n, k - 1);
            if (k >= 0 && n >= 0)
            {
                if (k == 1) return n;
                return CountOfCombinations(n - 1, k) + CountOfCombinations(n - 1, k - 1);
            }
            return CountOfCombinations(n+1,k+1)-CountOfCombinations(n, k+1);
        }
    }

    public class Graphs
    {
        Comb combAlg;

        public Graphs()
        {
            combAlg = new Comb();
        }

        public long NumberOfConnectedGraphs(int n)
        {
            if (n == 1 || n == 2) return 1;
            long sum = 0;
            for (int k = 1; k <= n-1; k++)
            {
                sum += k * combAlg.CountOfCombinations(n, k) * (long)Math.Pow(2, combAlg.CountOfCombinations(n - k, 2)) * NumberOfConnectedGraphs(k);
            }
            return (long)Math.Pow(2, combAlg.CountOfCombinations(n, 2))-(sum/n);
        }
        public long NumberOfEulerianGraphs(int n)
        {
            if (n == 1) return 1;
            if (n == 2) return 0;
            long sum = 0;
            for (int k = 1; k <= n - 1; k++)
            {
                sum += k * combAlg.CountOfCombinations(n, k) * (long)Math.Pow(2, combAlg.CountOfCombinations(n - k -1, 2)) * NumberOfEulerianGraphs(k);
            }
            return (long)Math.Pow(2, combAlg.CountOfCombinations(n-1, 2)) - (sum / n);
        }
        public long NumberOfEulerianGraphs(int n, int k)
        {
            //Инициализирую массив пар степеней членов ряда
            Tuple<int, int>[] degrees = new Tuple<int, int>[n + 1];

            //Заполняю массив степеней
            int maxDegree = (int)combAlg.CountOfCombinations(n, 2);
            for (int i = 0; i <= n; i++)
            {
                int first = maxDegree - (i * (n - i));
                int second = i * (n - i);
                degrees[i] = new Tuple<int, int>(first, second);
            }

            //Инициализация конечной суммы
            long sum = 0;
            int index = 0;
            foreach (Tuple<int,int> degree in degrees)
            {
                //Объявление коэффициента перед x^k в многочлене степени из degrees[index]
                long ratio = 0;
                //Расчет коэфициента с помощью бинома ньютона
                for (int i = 0; i <= degree.Item1 && i <= degree.Item2 && i <= k; i++)
                {
                    if (i % 2 == 0) ratio += combAlg.CountOfCombinations(degree.Item1, k - i) * combAlg.CountOfCombinations(degree.Item2, i);
                    else ratio -= combAlg.CountOfCombinations(degree.Item1, k - i) * combAlg.CountOfCombinations(degree.Item2, i);
                }
                //Сложение всех частичных коэффициентов
                sum += ratio * combAlg.CountOfCombinations(n, index);
                index++;
            }

            //Воозвращение результата
            return sum/(long)Math.Pow(2, n);
        }
        public long NumberOfConnectedGraphsWithColors(int n, int k)
        {
            if (k == 1) return 1;
            if (n < k) return 0;
            
            long sum = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                sum += combAlg.CountOfCombinations(n, i) * (long)Math.Pow(2, i * (n - i)) * NumberOfConnectedGraphsWithColors(i, k - 1);
            }
            return sum/k;
        }

    }
}
