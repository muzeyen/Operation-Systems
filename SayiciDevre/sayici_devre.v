module  sayici_devre (QA, QB, QC, Count, rst);
  output  QA, QB, QC;
  input  Count, rst; 
  JK_ff FA (QB, QB, Count, rst, QA);
  JK_ff FB ( 1, 1, Count, rst,QB);
  JK_ff FC (~QC,0, Count, rst, QC);   
 endmodule 