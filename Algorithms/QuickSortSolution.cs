namespace Algorithms
{
    public class QuickSortSolution : ISolution<int[], int[]>
    {
        public int[] Solve(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
            return array;
        }

        private void QuickSort(int[] array, int start, int end)
        {
            //base cases
            //empty or 1 element subarray
            if (start >= end)
            {
                return;
            }

            //2 element subarray
            if (end == start + 1)
            {
                if (array[start] > array[end])
                {
                    Swap(array, start, end);
                }

                return;
            }

            //swap elements around so that elements smaller than the pivot are placed to the left of the pivot and bigger to the right (i.e. partition around the pivot)
            int pivotIndex = Partition(array, start, end);

            //recurse on the left side of the array
            QuickSort(array, start, pivotIndex - 1);

            //recurse on the right side of the array
            QuickSort(array, pivotIndex + 1, end);
        }

        private int Partition(int[] array, int start, int end)
        {
            //initialize two pointers at the start and the end respectively
            int pivotIndex = ChoosePivot(array, start, end);
            int pivot = array[pivotIndex];
            int left = start + 1;
            int right = end;

            while (left < right)
            {
                while (left <= end && array[left] <= pivot)
                {
                    left++;
                }

                while (right > start && array[right] >= pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    Swap(array, left, right);
                }
            }

            //place pivot
            Swap(array, start, right);

            return right;
        }

        private void Swap(int[] array, int first, int second)
        {
            int temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }

        private int ChoosePivot(int[] array, int start, int end)
        {
            //pick the first element as the pivot
            return start;
        }
    }
}
