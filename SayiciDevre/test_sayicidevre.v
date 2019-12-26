module  test_sayicidevre;  
 reg  Count;  
 reg  rst;  
 wire   QA, QB ;
 reg QC;
sayici_devre s1 (QA, QB, QC, Count, rst);
  always   
 #5 Count = ~Count;
 
  initial 
   begin   
 Count = 1'b0;  
	QC = 1'b1;
 rst = 1'b1;  
 #5 rst = 1'b0;   
 end 
 initial  #200  $finish; 
 endmodule
 