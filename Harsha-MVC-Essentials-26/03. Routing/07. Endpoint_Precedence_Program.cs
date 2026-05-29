/*   

============= ENDPOINT SELECTION ORDER ==================

1. URL Template with more segments
   eg: "a/b/c/d" is higher than "a/b/c"

2. URL template with literal text has more precendence than a parameter segment/
   eg: "a/b" is higher than "a/{parameter}".

3. URL template that has a parameter segment with constraints has more 
   precedence than a parameter segment without constraints.
   eg: "a/{b:int}" is higher than "a/{b}".

4. Catch-all paramteres (**).
   eg: "a/{b}" is higher than "a/**".










*/