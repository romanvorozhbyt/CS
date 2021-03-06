// ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <ctime>
#include <iomanip>

using namespace std;
int main()
{
	srand(time(NULL));
	setlocale(LC_ALL, "rus");
	int size_array = 1000;
	int *a = new int[size_array]; // одномерный динамический массив
	for (int counter = 0; counter < size_array; counter++)
	{
		a[counter] = (int)pow(-1,counter) * rand() % 100; // заполняем массив случайными числами
		//cout << setw(2) << a[counter]<< "  "; // вывод массива на экран
	}

	int ans = a[0],
		ans_l = 0,
		ans_r = 0,
		sum = 0,
		min_sum = 0,
		min_pos = -1;
	for (int r = 0; r < 1000; ++r) {
		sum += a[r];

		int cur = sum - min_sum;
		if (cur > ans) {
			ans = cur;
			ans_l = min_pos + 1;
			ans_r = r;
		}

		if (sum < min_sum) {
			min_sum = sum;
			min_pos = r;
		}
	}
	cout << "min sum : " << min_sum<<endl;
	return 0;
}

