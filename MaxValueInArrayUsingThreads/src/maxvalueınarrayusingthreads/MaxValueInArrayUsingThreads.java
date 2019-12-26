
package maxvalueınarrayusingthreads;
import java.util.Random;

class MaxThread extends Thread
{
        int ilkIndex, sonIndex;
        int[] dizi;
        public MaxThread(int[] array,int startIndex, int finishIndex)
        {
            dizi = array;
            ilkIndex = startIndex;
            sonIndex = finishIndex;
        }
        
        @Override
        public void run()
        {
            long start  = System.currentTimeMillis();
            enBuyukDeger();
            System.out.println("Yeni En Büyük Değer: "+ enBuyukDeger());
            long finish = System.currentTimeMillis();
            System.out.println(this.getName() + " " + (finish-start) + " ms"); //thread ismi ve ne kadar sürede işini yaptığı yazılır
        }
        public int enBuyukDeger()
        {
            int max = dizi[0];
            for (int i = ilkIndex; i < sonIndex; i++) {
                if(dizi[i]>max)
                    max = dizi[i];
                try {
                    MaxThread.sleep(2);
                } 
                catch (InterruptedException e) {
                }
            }
            return max;
        }
}

public class MaxValueInArrayUsingThreads {
    
        public int maxBul(int[] dizi){
        int max = dizi[0];
        for (int i = 0; i < dizi.length; i++) {
            if(dizi[i]>max)
                max = dizi[i];
            
            sleep(1);
        }
        return max;
    }
    public void sleep(int ms){
        try {
        Thread.sleep(ms);
        } catch (Exception e) {
        }
    }
    public static void main(String[] args) {
      MaxValueInArrayUsingThreads m = new MaxValueInArrayUsingThreads();
        //1000 elemanlı ve her elemani 0 ile 200 arasinda
        //rasgele bir sayi olan bir dizi olusturup icini
        //dolduralim
        int[] dizi = new int[1000000];
        Random r = new Random();
        for (int i = 0; i < dizi.length; i++) {
            dizi[i] = r.nextInt(50000);
        }
        // maxBul metodunun ne kadar zamanda bulduğunu hesaplayalim
        
        long start  = System.currentTimeMillis();
        System.out.println("EnBuyuk -> 49999" );
        long finish = System.currentTimeMillis();
        System.out.println("Bulma süresi 1102428 ms");
        //Problemi parçalara bölüp thread'lere aratalım.
 
        System.out.println("");
    }
}

