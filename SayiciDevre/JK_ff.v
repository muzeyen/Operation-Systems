module JK_ff (	input wire J,input wire K,input wire clk,input wire rst,output reg Q);				
	initial 
		Q <= 1'b0;
	always @(posedge clk) begin
		if(rst) begin
			Q <= 1'b0;
		end
		else begin
			case({J,K})
				2'b00 : begin
					Q <= Q;
				end
				2'b01 : begin
					Q <= 1'b0;
				end
				2'b10 : begin
					Q <= 1'b1;
				end
				2'b11 : begin
					Q <= ~Q;
				end
			endcase
		end
	end	
endmodule
