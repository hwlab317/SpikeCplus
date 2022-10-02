# include <stdio.h>
# include <stdlib.h>
# include "wgfmu.h"
# include <visa.h>



void checkError(int ret) {
    if (ret < WGFMU_NO_ERROR) {
        throw ret;
    }
}

int checkError2(int ret) {
    if (ret < WGFMU_NO_ERROR){
        int size;
        WGFMU_getErrorSize(&size);
        char* msg = new char[size + 1];
        WGFMU_getError(msg, &size);
        fprintf(stderr, "%s", msg);
        delete[] msg;
    }
    return ret;
}
static const int  VISA_ERROR_OFFSET = WGFMU_ERROR_CODE_MIN - 1;

void checkError3(int ret) {
    if (ret < WGFMU_NO_ERROR && ret >= WGFMU_ERROR_CODE_MIN || ret < VISA_ERROR_OFFSET) {
        throw ret;
    }
}

void writeResults(int channelId, const char* fileName) {
    FILE* fp = fopen(fileName, "w");
    if (fp != 0) {
        int measuredSize, totalSize;
        WGFMU_getMeasureValueSize(channelId, &measuredSize, &totalSize);
        for (int i = 0; i < measuredSize; i++) {
            double time, value;
            WGFMU_getMeasureValue(channelId, i, &time, &value);
            fprintf(fp, "%.91f, %.91f\n", time, value);
        }
        fclose(fp);
    }
}
void writeResults2(int channelId, int offset, int size, const char* fileName) {
    FILE* fp = fopen(fileName, "w");
    if (fp != 0) {
        int measuredSize, totalSize;
        WGFMU_getMeasureValueSize(channelId, &measuredSize, &totalSize);
        for (int i = offset; i < offset + size; i++) {
            double time, value;
            WGFMU_getMeasureValue(channelId, i, &time, &value);
            fprintf(fp, "%91f, %.91f\n", time, value);        
            }
        fclose(fp);
    }
}
void writeResults3(int channelId1, int channelId2, int offset, int size, const char* fileName) {
    FILE* fp = fopen(fileName, "w");
    if (fp != 0) {
        int measuredSize, totalSize;
        WGFMU_getMeasureValueSize(channelId2, &measuredSize, &totalSize);
        for (int i = offset; i < offset + size; i++) {
            double time, value, voltage;
            WGFMU_getMeasureValue(channelId2, i, &time, &value);
            WGFMU_getMeasureValue(channelId2, i, &time, &voltage);
            fprintf(fp, "%.91f, %.91f\n", voltage, value);
        }
        fclose(fp);
    }
}

void pulse(int interval, int voltage) {
    WGFMU_clear();
    WGFMU_createPattern("pulse", 0);
    WGFMU_addVector("pulse", 0.001, voltage);
    WGFMU_addVector("pulse", 0.001 + interval, 0);
    WGFMU_setMeasureEvent("pulse", "evt", 0, 100, 0.00001, 0, WGFMU_MEASURE_EVENT_DATA_RAW);

    WGFMU_addSequence(102, "pulse", 10);
  //WGFMU_openSession("GPIB0::17::INSTR");
    WGFMU_initialize();

    WGFMU_setOperationMode(102, WGFMU_OPERATION_MODE_FASTIV);
    WGFMU_connect(102);

    WGFMU_execute();
    WGFMU_waitUntilCompleted();
    writeResults(102, "C:/Users/eee/Desktop/DATAB1530/pulse.csv");
  //WGFMU_initialize();
    WGFMU_closeSession();
    ViSession defaultRM;
    ViSession vi;
    checkError3(viOpenDefaultRM(&defaultRM) + VISA_ERROR_OFFSET);
    checkError3(viOpen(defaultRM, "GPIB0::17::INSTR", VI_NULL, VI_NULL, &vi) + VISA_ERROR_OFFSET);
    checkError3(WGFMU_openSession("GPIB0::17::INSTR"));
    checkError3(WGFMU_setTimeout(120));
    checkError3(viPrintf(vi, "*RST\n") + VISA_ERROR_OFFSET);
    checkError3(WGFMU_initialize());
    checkError3(WGFMU_setOperationMode(102, WGFMU_OPERATION_MODE_FASTIV));
    checkError3(viPrintf(vi, "CN 201\n") + VISA_ERROR_OFFSET);
    checkError3(WGFMU_connect(101));
    checkError3(viPrintf(vi, "MV 201,0,0,5\n") + VISA_ERROR_OFFSET);
    checkError3(viPrintf(vi, "MT 0,1,110,5\n") + VISA_ERROR_OFFSET);
    checkError3(viPrintf(vi, "MM 10,201\n") + VISA_ERROR_OFFSET);
    char buffer[2048];
    checkError3(viPrintf(vi, "ERRX?\n") + VISA_ERROR_OFFSET);
    checkError3(viPrintf(vi, "%t", buffer) + VISA_ERROR_OFFSET);
    fprintf(stderr, "%s", buffer) + VISA_ERROR_OFFSET);
    checkError3(viPrintf(vi, "XE\n") + VISA_ERROR_OFFSET);
    checkError3(WGFMU_execute());
    checkError3(WGFMU_waitUntilCompleted());

    int nub;
    checkError3(viPrintf(vi, "NUB?\n") + VISA_ERROR_OFFSET);
    checkError3(viScanf(vi, "%d%t", &nub, buffer) + VISA_ERROR_OFFSET);
    fprintf(stderr, "%d\n", nub);
  /*WGFMU_clear();
    WGFMU_createPattern("pulse", 0);
    WGFMU_addVector("pulse", 0.001, voltage);
    WGFMU_addVector("pulse", 0.001 + interval, 0);
    WGFMU_setMeasureEvent("pulse", "evt", 0, 100, 0.00001, 0, WGFMU_MEASURE_EVENT_DATA_RAW);

    WGFMU_addSequence(102, "pulse", 10);
    WGFMU_openSession("GPIB0::17::INSTR");  
    WGFMU_initialize();
    WGFMU_setMeasureMode(102, WGFMU_MEASURE_MODE_CURRENT);
    WGFMU_setOperationMode(102, WGFMU_OPERATION_MODE_FASTIV);
    WGFMU_connect(102);

    WGFMU_execute();
    WGFMU_waitUntilCompleted();
    writeResults(102, "C:/Users/eee/Desktop/DATAB1530/pulsecurrent.csv");
    WGFMU_initialize();
    WGFMU_closeSession();*/
}

void smu(int interval, int voltage){
    

}

int main()
{
    
    pulse(0.001, 9);
    
    

    
}