
package maxprimaryvalueınarrayusingthreads;
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
            enBuyukAsalDeger();
            System.out.println("Yeni En Büyük Asal Değer: "+ enBuyukAsalDeger());
            long finish = System.currentTimeMillis();
            System.out.println(this.getName() + " " + (finish-start) + " ms"); //thread ismi ve ne kadar sürede işini yaptığı yazılır
        }
        public int enBuyukAsalDeger()
        {
            int max = 0;
            boolean asal=true;
            for (int i = ilkIndex; i < sonIndex; i++) {
                if (dizi[i] <= 1)
                    i++;
                else{
                    for (int j = 2; j < dizi[i]; j++) {
                        int kalan = dizi[i] % j;
                        if (kalan == 0){
                            asal=false;
                            break;
                        }                           
                    }
                    if(asal==true){
                       if(max<dizi[i])
                           max=dizi[i];
                    }
                    try {
                    MaxThread.sleep(2);
                    } 
                    catch (InterruptedException e) {
                    }
                }               
            }
            return max;
        }
}
public class MaxPrimaryValueInArrayUsingThreads {

    public static void main(String[] args) {
        int[] randomDizi = new int[1000];
        Random random = new Random();
        for (int i = 0; i < randomDizi.length; i++) 
        {
            randomDizi[i] = random.nextInt(1000); //dizinin içerisinde maksimum 1000 olabilecek rastgele sayılar ürettik
        }
        //En büyük sayıyı bulmak için diziyi parçalara bölüp thread'lere aratarsak
  
        MaxThread eightThread1 = new MaxThread(randomDizi, 0, 125);
        MaxThread eightThread2 = new MaxThread(randomDizi, 125, 250);
        MaxThread eightThread3 = new MaxThread(randomDizi, 250, 375);
        MaxThread eightThread4 = new MaxThread(randomDizi, 375, 500);
        MaxThread eightThread5 = new MaxThread(randomDizi, 500, 625);
        MaxThread eightThread6 = new MaxThread(randomDizi, 625, 750);
        MaxThread eightThread7 = new MaxThread(randomDizi, 750, 875);
        MaxThread eightThread8 = new MaxThread(randomDizi, 875, 1000);
        System.out.println("");
        System.out.println("8 THREAD ÇALIŞIRSA");
        
  
    }   
}
