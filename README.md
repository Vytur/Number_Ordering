# DanskeBank Assignment - Number Sorting Web API

This project provides a Web API for sorting numbers using multiple algorithms and saving the results to a file. It is developed in the latest .NET version using Visual Studio, following best practices.

## Features

1. **Sort and Save Numbers**:
   - Input a line of numbers (e.g., `[5, 2, 8, 10, 1]`).
   - Numbers are sorted (e.g., `1, 2, 5, 8, 10`) and saved to a file (`result.txt`).

2. **Retrieve Latest Results**:
   - Load and view the content of the latest saved file.

3. **Multiple Sorting Algorithms**:
   - Algorithms include **Bubble Sort**, **Quick Sort**, and **Merge Sort**.
   - Sorting time and memory usage are measured for each algorithm.


### **Sort Numbers**

**Project contains a http file with request examples**

**POST** `/api/numbers/sort?algorithm={SortingAlgorithm}`

- **Request Body** (JSON):  
  An array of integers, e.g., `[5, 2, 8, 10, 1]`.

- **Query Parameter**:
  - `SortingAlgorithm` (optional): Choose a specific algorithm (`BubbleSort`, `QuickSort`, or `MergeSort`). If not provided, all algorithms are executed.

- **Response**:  
  A JSON array with sorting results for each algorithm:
  ```json
  [
    {
      "Algorithm": "QuickSort",
      "SortedNumbers": [1, 2, 5, 8, 10],
      "ExecutionTime": 100,
      "MemoryUsage": 2048
    }
  ] 
  
  ```json

 - **Parameters**:
  - ExecutionTime - nanoseconds
  - MemomyUsage - bytes

**GET** `/api/numbers/latest`

 - Get Latest File Content

   ```json
    {
       "Content": "1 2 5 8 10"
    }
   ```json