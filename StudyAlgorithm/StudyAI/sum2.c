//#define _CRT_SECURE_NO_WARNINGS
//
///* ヘッダファイルのインクルード */
//#include <stdio.h>
//#include <stdlib.h>
//
///* 記号定数の定義 */
//#define BUFSIZE 256
//
//int main()
//{
//    char linebuf[BUFSIZE];  /* 入力バッファ */
//    double data;  /* 入力データ */
//    double sum = 0.0;  /* 和 */
//    double sum2 = 0.0;  /* 2乗和 */
//
//    while (fgets(linebuf, BUFSIZE, stdin) != NULL)
//    {
//        /* 行の読み取りが可能な間繰り返す */
//        if (sscanf(linebuf, "%lf", &data) != 0)
//        {
//            /* 変換出来たら */
//            sum += data;
//            sum2 += data * data;
//            printf("%f\t%f\n", sum, sum2);
//        }
//    }
//
//    return 0;
//}
