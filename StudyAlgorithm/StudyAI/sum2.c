//#define _CRT_SECURE_NO_WARNINGS
//
///* �w�b�_�t�@�C���̃C���N���[�h */
//#include <stdio.h>
//#include <stdlib.h>
//
///* �L���萔�̒�` */
//#define BUFSIZE 256
//
//int main()
//{
//    char linebuf[BUFSIZE];  /* ���̓o�b�t�@ */
//    double data;  /* ���̓f�[�^ */
//    double sum = 0.0;  /* �a */
//    double sum2 = 0.0;  /* 2��a */
//
//    while (fgets(linebuf, BUFSIZE, stdin) != NULL)
//    {
//        /* �s�̓ǂݎ�肪�\�ȊԌJ��Ԃ� */
//        if (sscanf(linebuf, "%lf", &data) != 0)
//        {
//            /* �ϊ��o������ */
//            sum += data;
//            sum2 += data * data;
//            printf("%f\t%f\n", sum, sum2);
//        }
//    }
//
//    return 0;
//}
