��u s i n g   S y s t e m ;  
 u s i n g   S y s t e m . I O ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   M y S q l . D a t a . M y S q l C l i e n t ;  
  
 p u b l i c   c l a s s   T a b l e R e a d e r  
 {  
 	 / / I n i t i a l i z e   t h e   c o n n e c t i o n  
 	 s t r i n g   c o n n e c t i o n S t r i n g   =   " d a t a s o u r c e = 1 2 7 . 0 . 0 . 1 ; p o r t = 3 3 0 6 ; u s e r n a m e = r o o t ; p a s s w o r d = ; d a t a b a s e = n p p e s _ 1 ; " ;  
  
 	 / / M y S q l D a t a R e a d e r   r e a d e r ;  
 	 M y S q l C o n n e c t i o n   d a t a b a s e C o n n e c t i o n ;  
 	 M y S q l C o m m a n d   c o m m a n d   =   n e w   M y S q l C o m m a n d ( ) ;  
  
 	 O r g a n i z a t i o n M a n a g e r   o r g M a n a g e r ;  
 	 P r o v i d e r M a n a g e r   p r o M a n a g e r ;  
 	 D e a c t i v a t i o n M a n a g e r   d e a M a n a g e r ;  
  
 	 p u b l i c   T a b l e R e a d e r ( s t r i n g   c o n n e c t i o n S t r i n g )  
 	 {  
 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y ( c o n n e c t i o n S t r i n g ) )  
 	 	 {  
 	 	 	 t h i s . c o n n e c t i o n S t r i n g   =   c o n n e c t i o n S t r i n g ;  
 	 	 }    	 	 d a t a b a s e C o n n e c t i o n   =   n e w   M y S q l C o n n e c t i o n ( t h i s . c o n n e c t i o n S t r i n g ) ;  
 	 	 o r g M a n a g e r   =   n e w   O r g a n i z a t i o n M a n a g e r ( " n p i _ o r g a n i z a t i o n _ d a t a " ) ;  
 	 	 p r o M a n a g e r   =   n e w   P r o v i d e r M a n a g e r ( " n p i _ p r o v i d e r _ d a t a " ) ;  
 	 	 d e a M a n a g e r   =   n e w   D e a c t i v a t i o n M a n a g e r ( " n p i _ d e a c t i v a t e d " ) ;  
 	 }  
  
 	 p r i v a t e   v o i d   r e a d U p d a t e F i l e ( s t r i n g   f i l e L o c a t i o n )  
 	 {  
 	 	 L i s t < E n t r y >   e n t r i e s   =   g e n e r a t e E n t r i e s ( f i l e L o c a t i o n ) ;  
  
 	 	 f o r e a c h   ( E n t r y   c u r E n t r y   i n   e n t r i e s )  
 	 	 {  
 	 	 	 s w i t c h   ( c u r E n t r y . e n t r y T y p e )   {  
 	 	 	 c a s e   E n t r y T y p e . P r o v i d e r :  
 	 	 	 	 {  
 	 	 	 	 	 u p d a t e T a b l e ( p r o M a n a g e r ,   c u r E n t r y ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 }  
 	 	 	 c a s e   E n t r y T y p e . O r g a n i z a t i o n :  
 	 	 	 	 {  
 	 	 	 	 	 u p d a t e T a b l e ( o r g M a n a g e r ,   c u r E n t r y ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 }  
  
 	 	 	 c a s e   E n t r y T y p e . D e a c t i v a t e :  
 	 	 	 	 {  
 	 	 	 	 	 d e a c t i v a t e E n t i t y   ( c u r E n t r y ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
 	 }  
  
 	 p r i v a t e   v o i d   r e a d D e a c t i v a t i o n F i l e ( s t r i n g   f i l e L o c a t i o n )  
 	 {  
 	 	 L i s t < E n t r y >   e n t r i e s   =   g e n e r a t e E n t r i e s ( f i l e L o c a t i o n ) ;  
  
 	 	 / / A s   e a c h   e n t r y   i n   t h e   d e a c t i v a t i o n   l i s t   i s   k n o w   t o   b e   o f   E n t r y T y p e . D e a c t i v a t e ,   a   s w i t c h   c h e c k   i s   u n n e c c e s a r y  
 	 	 f o r e a c h ( E n t r y   c u r E n t r y   i n   e n t r i e s ) {  
 	 	 	 d e a c t i v a t e E n t i t y ( c u r E n t r y ) ;  
 	 	 }  
 	 }  
  
 	 / / P o p u l a t e s   t h e   d a t a b a s e   f r o m   t h e   f u l l   f i l e .     S h o u l d   b e   r u n   a f t e r   t h e   a p p l y S c h e m a   h a s   b e e n   r u n .  
 	 p r i v a t e   v o i d   r e a d F u l l F i l e ( s t r i n g   f i l e L o c a t i o n )  
 	 {  
 	 	 L i s t < E n t r y >   e n t r i e s   =   g e n e r a t e E n t r i e s ( f i l e L o c a t i o n ) ;  
  
 	 	 f o r e a c h   ( E n t r y   c u r E n t r y   i n   e n t r i e s )  
 	 	 {  
 	 	 	 s w i t c h   ( c u r E n t r y . e n t r y T y p e )   {  
 	 	 	 c a s e   E n t r y T y p e . P r o v i d e r :  
 	 	 	 	 {  
 	 	 	 	 	 a d d T o T a b l e ( p r o M a n a g e r ,   c u r E n t r y ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 }  
 	 	 	 c a s e   E n t r y T y p e . O r g a n i z a t i o n :  
 	 	 	 	 {  
 	 	 	 	 	 a d d T o T a b l e ( o r g M a n a g e r ,   c u r E n t r y ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 }  
 	 	 	 c a s e   E n t r y T y p e . D e a c t i v a t e :  
 	 	 	 	 {  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
 	 }  
  
 	 p r i v a t e   v o i d   u p d a t e T a b l e ( I D a t a M a n a g e r   t a b l e M a n a g e r ,   E n t r y   e n t r y )  
 	 {  
 	 	 / / I f   t h e   d a t a b a s e   w a s   n o t   a b l e   t o   u p d a t e   t h e   e n t i t y ,   a t t e m p t   t o   a d d   i t   i n s t e a d  
 	 	 i f   ( ! t r y C o m m a n d ( t a b l e M a n a g e r . U p d a t e E n t i t y ( e n t r y ) ) )   {  
 	 	 	 t r y C o m m a n d ( t a b l e M a n a g e r . A d d E n t i t y ( e n t r y ) ) ;  
 	 	 }      
 	 }  
  
 	 p r i v a t e   v o i d   a d d T o T a b l e ( I D a t a M a n a g e r   t a b l e M a n a g e r ,   E n t r y   e n t r y )  
 	 {  
 	 	 t r y C o m m a n d ( t a b l e M a n a g e r . A d d E n t i t y ( e n t r y ) ) ;  
 	 }  
  
 	 p r i v a t e   L i s t < E n t r y >   g e n e r a t e E n t r i e s ( s t r i n g   f i l e L o c a t i o n )  
 	 {  
 	 	 / / I n i t i a l i z e   t h e   S t r e a m R e a d e r  
 	 	 F i l e S t r e a m   f s   =   F i l e . O p e n R e a d ( f i l e L o c a t i o n ) ;  
 	 	 S t r e a m R e a d e r   s r   =   n e w   S t r e a m R e a d e r ( f s ) ;  
  
 	 	 / / R e a d s   i n   t h e   C S V   u p d a t e   f i l e ,   l i n e   b y   l i n e  
 	 	 L i s t < E n t r y >   e n t r i e s   =   n e w   L i s t < E n t r y > ( ) ;  
 	 	 w h i l e   ( ! s r . E n d O f S t r e a m )  
 	 	 {  
 	 	 	 / / T O D O :   W h e n   g e n e r a t i n g   e n t r i e s   f o r   d e a c t i v a t i o n   f i l e s ,   D e t e r m i n e   a n d   u t i l i z e   t h e   a p p l i c a b l e   s t o p   d a t e  
 	 	 	 s t r i n g   l i n e   =   s r . R e a d L i n e ( ) ;  
 	 	 	 E n t r y   e n t r y   =   n e w   E n t r y ( n e w   L i s t < s t r i n g > ( l i n e . S p l i t ( ' , ' ) ) ) ;  
 	 	 	 e n t r i e s . A d d ( e n t r y ) ;  
 	 	 }  
  
 	 	 r e t u r n   e n t r i e s ;  
 	 }  
  
 	 p r i v a t e   v o i d   d e a c t i v a t e E n t i t y ( E n t r y   e n t r y ) {  
  
 	 	 i f   ( ! t r y C o m m a n d   ( d e a M a n a g e r . U p d a t e E n t i t y ( e n t r y ) ) )   {  
 	 	 	 t r y C o m m a n d   ( d e a M a n a g e r . A d d E n t i t y   ( e n t r y ) ) ;  
 	 	 }  
  
 	 	 t r y C o m m a n d ( p r o M a n a g e r . D e a c t i v a t e E n t i t y   ( e n t r y . N P I ) ) ;  
 	 	 t r y C o m m a n d ( o r g M a n a g e r . D e a c t i v a t e E n t i t y   ( e n t r y . N P I ) ) ;  
 	 }  
  
 	 p r i v a t e   b o o l   t r y C o m m a n d ( s t r i n g   q u e r y ) {  
  
 	 	 / / S t o r e s   t h e   n u m b e r   o f   e f f e c t e d   r o w s   b y   t h e   e x e c u t e d   c o m m a n d  
 	 	 i n t   n u m M a t c h e d ;  
  
 	 	 c o m m a n d   =   n e w   M y S q l C o m m a n d ( q u e r y ,   d a t a b a s e C o n n e c t i o n ) ;  
 	 	 c o m m a n d . C o m m a n d T i m e o u t   =   3 0 ;  
  
 	 	 t r y {  
 	 	 	 d a t a b a s e C o n n e c t i o n . O p e n ( ) ;  
 	 	 	 / / r e a d e r   =   c o m m a n d . E x e c u t e R e a d e r ( ) ;  
 	 	 	 n u m M a t c h e d   =   c o m m a n d . E x e c u t e N o n Q u e r y ( ) ;  
  
 	 	 	 d a t a b a s e C o n n e c t i o n . C l o s e ( ) ;  
 	 	 }  
 	 	 c a t c h   ( E x c e p t i o n   e x ) {  
 	 	 	 d a t a b a s e C o n n e c t i o n . C l o s e ( ) ;  
 	 	 	 r e t u r n   f a l s e ;  
 	 	 }  
  
 	 	 / / O n l y   o n e   r o w   s h o u l d   e v e r   b e   e f f e c t e d   b y   a   g i v e n   c o m m a n d ,   a s   N P I   i s   t h e   p r i m a r y   k e y   a n d   p a s s e d   a s   t h e   a r g u m e n t  
 	 	 i f   ( n u m M a t c h e d   = =   1 )   {  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }   e l s e   {  
 	 	 	 r e t u r n   f a l s e ;  
 	 	 }  
 	 }  
  
 	 / / D e l e t e s   r o w s   t h a t   d o n ' t   b e l o n g   i n   t h e i r   r e s p e c t i v e   t a b l e s ,   i e .   r o w s   w i t h   p r o v i d e r   d a t a   d o n ' t   b e l o n g   i n   t h e   o r g a n i z a t i o n   t a b l e .  
 	 p r i v a t e   v o i d   r e m o v e D u p l i c a t e s ( ) {  
  
 	 	 M y S q l C o m m a n d   c o m m a n d P r o v i d e r   =   n e w   M y S q l C o m m a n d ( ) ;  
 	 	 M y S q l C o m m a n d   c o m m a n d O r g a n i z a t i o n   =   n e w   M y S q l C o m m a n d ( ) ;  
  
 	 	 s t r i n g   p r o v i d e r Q u e r y   =   " D E L E T E   F R O M   n p i _ p r o v i d e r _ d a t a   W H E R E   P r o v i d e r L a s t N a m e   =   ' ' ; " ;  
 	 	 s t r i n g   o r g a n i z a t i o n Q u e r y   =   " D E L E T E   F R O M   n p i _ o r g a n i z a t i o n _ d a t a   W H E R E   N a m e   =   ' ' ; " ;  
  
 	 	 c o m m a n d P r o v i d e r   =   n e w   M y S q l Q u e r y ( p r o v i d e r Q u e r y ,   d a t a b a s e C o n n e c t i o n ) ;  
 	 	 c o m m a n d P r o v i d e r . C o m m a n d T i m e o u t   =   I n t 3 2 . M a x V a l u e ;  
  
 	 	 c o m m a n d O r g a n i z a t i o n   =   n e w   M y S q l Q u e r y ( o r g a n i z a t i o n Q u e r y ,   d a t a b a s e C o n n e c t i o n ) ;  
 	 	 c o m m a n d O r g a n i z a t i o n . C o m m a n d T i m e o u t   =   I n t 3 2 . M a x V a l u e ;  
  
 	 	 t r y {  
 	 	 	 d a t a b a s e C o n n e c t i o n . O p e n ( ) ;  
 	 	 	 / / r e a d e r   =   c o m m a n d . E x e c u t e R e a d e r ( ) ;  
 	 	 	 c o m m a n d P r o v i d e r . E x e c u t e N o n Q u e r y ( ) ;  
 	 	 	 c o m m a n d O r g a n i z a t i o n . E x e c u t e N o n Q u e r y ( ) ;  
  
 	 	 	 d a t a b a s e C o n n e c t i o n . C l o s e ( ) ;  
 	 	 }  
 	 	 c a t c h   ( E x c e p t i o n   e x ) {  
 	 	 	 d a t a b a s e C o n n e c t i o n . C l o s e ( ) ;  
 	 	 }  
 	 }  
 	 	  
 	 / / D e l e t e s   t h e   e n t i r e   d a t a b s e   a n d   r e a p p l i e s   t h e   s c h e m a .  
 	 p r i v a t e   v o i d   a p p l y S c h e m a ( ) {  
 	 	 s t r i n g   d e l e t e D a t a b a s e   =   " D R O P   D A T A B A S E   n p p e s _ 1 " ;  
 	 	 s t r i n g   s c h e m a   =   " C R E A T E   D A T A B A S E     I F   N O T   E X I S T S   ` n p p e s _ 1 `   / * ! 4 0 1 0 0   D E F A U L T   C H A R A C T E R   S E T   u t f 8   C O L L A T E   u t f 8 _ u n i c o d e _ c i   * / ; "   +  
 	 	 	 " \ n U S E   ` n p p e s _ 1 ` ; \ n - -   M y S Q L   d u m p   1 0 . 1 3     D i s t r i b   5 . 7 . 9 ,   f o r   W i n 6 4   ( x 8 6 _ 6 4 ) \ n - - \ n - -   H o s t :   1 9 2 . 1 6 8 . 1 . 1 5 1         D a t a b a s e :   n p p e "   +  
 	 	 	 " s \ n - -   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - \ n - -   S e r v e r   v e r s i o n \ t 5 . 6 . 2 6 - l o g \ n \ n / * ! 4 0 1 0 1   S E T   @ O L D _ C H A R A "   +  
 	 	 	 " C T E R _ S E T _ C L I E N T = @ @ C H A R A C T E R _ S E T _ C L I E N T   * / ; \ n / * ! 4 0 1 0 1   S E T   @ O L D _ C H A R A C T E R _ S E T _ R E S U L T S = @ @ C H A R A C T E R _ S E T _ R E S U L T S   * / ; \ n / * ! 4 0 "   +  
 	 	 	 " 1 0 1   S E T   @ O L D _ C O L L A T I O N _ C O N N E C T I O N = @ @ C O L L A T I O N _ C O N N E C T I O N   * / ; \ n / * ! 4 0 1 0 1   S E T   N A M E S   u t f 8   * / ; \ n / * ! 4 0 1 0 3   S E T   @ O L D _ T I M E _ Z O N E "   +  
 	 	 	 " = @ @ T I M E _ Z O N E   * / ; \ n / * ! 4 0 1 0 3   S E T   T I M E _ Z O N E = ' + 0 0 : 0 0 '   * / ; \ n / * ! 4 0 0 1 4   S E T   @ O L D _ U N I Q U E _ C H E C K S = @ @ U N I Q U E _ C H E C K S ,   U N I Q U E _ C H E C K S = "   +  
 	 	 	 " 0   * / ; \ n / * ! 4 0 0 1 4   S E T   @ O L D _ F O R E I G N _ K E Y _ C H E C K S = @ @ F O R E I G N _ K E Y _ C H E C K S ,   F O R E I G N _ K E Y _ C H E C K S = 0   * / ; \ n / * ! 4 0 1 0 1   S E T   @ O L D _ S Q L _ M O D E "   +  
 	 	 	 " = @ @ S Q L _ M O D E ,   S Q L _ M O D E = ' N O _ A U T O _ V A L U E _ O N _ Z E R O '   * / ; \ n / * ! 4 0 1 1 1   S E T   @ O L D _ S Q L _ N O T E S = @ @ S Q L _ N O T E S ,   S Q L _ N O T E S = 0   * / ; \ n \ n - - \ n - -   "   +  
 	 	 	 " T a b l e   s t r u c t u r e   f o r   t a b l e   ` n p i _ o r g a n i z a t i o n _ d a t a ` \ n - - \ n \ n D R O P   T A B L E   I F   E X I S T S   ` n p i _ o r g a n i z a t i o n _ d a t a ` ; \ n / * ! 4 0 1 0 1   S E T   @ "   +  
 	 	 	 " s a v e d _ c s _ c l i e n t           =   @ @ c h a r a c t e r _ s e t _ c l i e n t   * / ; \ n / * ! 4 0 1 0 1   S E T   c h a r a c t e r _ s e t _ c l i e n t   =   u t f 8   * / ; \ n C R E A T E   T A B L E   ` n p i _ o r g a n "   +  
 	 	 	 " i z a t i o n _ d a t a `   ( \ n     ` N P I `   i n t ( 1 0 )   u n s i g n e d   N O T   N U L L , \ n     ` N a m e `   v a r c h a r ( 7 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   N O T   N U L L , \ n     ` O t h e r N a "   +  
 	 	 	 " m e `   v a r c h a r ( 7 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` O t h e r N a m e T y p e C o d e `   v a r c h a r ( 1 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T "   +  
 	 	 	 "   N U L L , \ n     ` F i r s t L i n e M a i l i n g A d d r e s s `   v a r c h a r ( 5 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` S e c o n d L i n e M a i l i n g A d d r e s s `   v a r "   +  
 	 	 	 " c h a r ( 5 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r e s s C i t y `   v a r c h a r ( 4 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , "   +  
 	 	 	 " \ n     ` M a i l i n g A d d r e s s S t a t e `   v a r c h a r ( 4 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r e s s P o s t a l C o d e `   v a r c h a r ( 2 0 )   C O "   +  
 	 	 	 " L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r e s s C o u n t r y C o d e `   v a r c h a r ( 2 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     "   +  
 	 	 	 " ` M a i l i n g A d d r e s s T e l e p h o n e `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r e s s F a x `   v a r c h a r ( 2 0 )   C O L L A T E   u "   +  
 	 	 	 " t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` F i r s t L i n e P r a c t i c e A d d r e s s `   v a r c h a r ( 5 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` S e c o n d L i "   +  
 	 	 	 " n e P r a c t i c e A d d r e s s `   v a r c h a r ( 5 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d r e s s C i t y `   v a r c h a r ( 4 0 )   C O L L A T E   u t f 8 _ u n "   +  
 	 	 	 " i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d r e s s S t a t e `   v a r c h a r ( 4 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d r e s s P "   +  
 	 	 	 " o s t a l C o d e `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d r e s s C o u n t r y C o d e `   v a r c h a r ( 2 )   C O L L A T E   u t f 8 _ u "   +  
 	 	 	 " n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d r e s s T e l e p h o n e `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d "   +  
 	 	 	 " r e s s F a x `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` A u t h o r i z e d O f f i c i a l L a s t N a m e `   v a r c h a r ( 3 5 )   C O L L A T E   u t f 8 _ u n "   +  
 	 	 	 " i c o d e _ c i   D E F A U L T   N U L L , \ n     ` A u t h o r i z e d O f f i c i a l F i r s t N a m e `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` A u t h o r i z "   +  
 	 	 	 " e d O f f i c i a l T i t l e `   v a r c h a r ( 3 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` A u t h o r i z e d O f f i c i a l C r e d e n t i a l `   v a r c h a r ( 2 0 )   C O L L A "   +  
 	 	 	 " T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` A u t h o r i z e d O f f i c i a l T e l e p h o n e `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n   "   +  
 	 	 	 "   ` T a x o n o m y C o d e 1 `   v a r c h a r ( 1 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` L i c e n s e N u m b e r 1 `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e "   +  
 	 	 	 " _ c i   D E F A U L T   N U L L , \ n     ` L i c e n s e S t a t e C o d e 1 `   v a r c h a r ( 2 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` T a x o n o m y S w i t c h 1 `   v a r c h a "   +  
 	 	 	 " r ( 1 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` I s S o l e P r o p r i e t o r `   v a r c h a r ( 1 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     "   +  
 	 	 	 " ` I s O r g a n i z a t i o n S u b p a r t `   v a r c h a r ( 1 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` D e a c t i v a t i o n D a t e `   d a t e t i m e   D E F A U L T   N U L L , \ "   +  
 	 	 	 " n     P R I M A R Y   K E Y   ( ` N P I ` ) \ n )   E N G I N E = I n n o D B   D E F A U L T   C H A R S E T = u t f 8   C O L L A T E = u t f 8 _ u n i c o d e _ c i   R O W _ F O R M A T = C O M P A C T ; \ n / * ! 4 0 1 0 1   S E T "   +  
 	 	 	 "   c h a r a c t e r _ s e t _ c l i e n t   =   @ s a v e d _ c s _ c l i e n t   * / ; \ n \ n - - \ n - -   T a b l e   s t r u c t u r e   f o r   t a b l e   ` n p i _ p r o v i d e r _ d a t a ` \ n - - \ n \ n D R O P   T A B L E "   +  
 	 	 	 "   I F   E X I S T S   ` n p i _ p r o v i d e r _ d a t a ` ; \ n / * ! 4 0 1 0 1   S E T   @ s a v e d _ c s _ c l i e n t           =   @ @ c h a r a c t e r _ s e t _ c l i e n t   * / ; \ n / * ! 4 0 1 0 1   S E T   c h a r a c t e r "   +  
 	 	 	 " _ s e t _ c l i e n t   =   u t f 8   * / ; \ n C R E A T E   T A B L E   ` n p i _ p r o v i d e r _ d a t a `   ( \ n     ` N P I `   i n t ( 1 0 )   u n s i g n e d   N O T   N U L L , \ n     ` P r o v i d e r L a s t N a m e `   v a "   +  
 	 	 	 " r c h a r ( 4 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r o v i d e r F i r s t N a m e `   v a r c h a r ( 4 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , "   +  
 	 	 	 " \ n     ` P r o v i d e r N a m e P r e f i x `   v a r c h a r ( 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r o v i d e r N a m e S u f f i x `   v a r c h a r ( 5 )   C O L L A T E   u t "   +  
 	 	 	 " f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r o v i d e r C r e d e n t i a l T e x t `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` F i r s t L i n "   +  
 	 	 	 " e M a i l i n g A d d r e s s `   v a r c h a r ( 5 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` S e c o n d L i n e M a i l i n g A d d r e s s `   v a r c h a r ( 5 5 )   C O L L A T E   u t "   +  
 	 	 	 " f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r e s s C i t y `   v a r c h a r ( 4 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r e "   +  
 	 	 	 " s s S t a t e `   v a r c h a r ( 4 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r e s s P o s t a l C o d e `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i "   +  
 	 	 	 " c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r e s s C o u n t r y C o d e `   v a r c h a r ( 2 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r "   +  
 	 	 	 " e s s T e l e p h o n e `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` M a i l i n g A d d r e s s F a x `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c "   +  
 	 	 	 " o d e _ c i   D E F A U L T   N U L L , \ n     ` F i r s t L i n e P r a c t i c e A d d r e s s `   v a r c h a r ( 5 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` S e c o n d L i n e P "   +  
 	 	 	 " r a c t i c e A d d r e s s `   v a r c h a r ( 5 5 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d r e s s C i t y `   v a r c h a r ( 4 5 )   C O L L A T E   u t f 8 "   +  
 	 	 	 " _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d r e s s S t a t e `   v a r c h a r ( 4 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A "   +  
 	 	 	 " d d r e s s P o s t a l C o d e `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d r e s s C o u n t r y C o d e `   v a r c h a r ( 2 )   C O L "   +  
 	 	 	 " L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` P r a c t i c e A d d r e s s T e l e p h o n e `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , "   +  
 	 	 	 " \ n     ` P r a c t i c e A d d r e s s F a x N u m b e r `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` T a x o n o m y C o d e 1 `   v a r c h a r ( 1 0 ) "   +  
 	 	 	 "   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` L i c e n s e N u m b e r 1 `   v a r c h a r ( 2 0 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     "   +  
 	 	 	 " ` L i c e n s e S t a t e C o d e 1 `   v a r c h a r ( 2 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` T a x o n o m y S w i t c h 1 `   v a r c h a r ( 1 )   C O L L A T E   u t f 8 _ u "   +  
 	 	 	 " n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` I s S o l e P r o p r i e t o r `   v a r c h a r ( 1 )   C O L L A T E   u t f 8 _ u n i c o d e _ c i   D E F A U L T   N U L L , \ n     ` D e a c t i v a t i o n D a t e ` "   +  
 	 	 	 "   d a t e t i m e   D E F A U L T   N U L L , \ n     P R I M A R Y   K E Y   ( ` N P I ` ) \ n )   E N G I N E = I n n o D B   D E F A U L T   C H A R S E T = u t f 8   C O L L A T E = u t f 8 _ u n i c o d e _ c i ; \ n / * ! 4 0 1 "   +  
 	 	 	 " 0 1   S E T   c h a r a c t e r _ s e t _ c l i e n t   =   @ s a v e d _ c s _ c l i e n t   * / ; \ n \ n - - \ n - -   T a b l e   s t r u c t u r e   f o r   t a b l e   ` n p i _ d e a c t i v a t e d ` \ n - - \ n \ n D R O "   +  
 	 	 	 " P   T A B L E   I F   E X I S T S   ` n p i _ d e a c t i v a t e d ` ; \ n / * ! 4 0 1 0 1   S E T   @ s a v e d _ c s _ c l i e n t           =   @ @ c h a r a c t e r _ s e t _ c l i e n t   * / ; \ n / * ! 4 0 1 0 1   S E T   c h a "   +  
 	 	 	 " r a c t e r _ s e t _ c l i e n t   =   u t f 8   * / ; \ n C R E A T E   T A B L E   ` n p i _ d e a c t i v a t e d `   ( \ n     ` N P I `   i n t ( 1 0 )   u n s i g n e d   N O T   N U L L , \ n     ` D e a c t i v a t i o n D "   +  
 	 	 	 " a t e `   d a t e t i m e   D E F A U L T   N U L L , \ n     P R I M A R Y   K E Y   ( ` N P I ` ) \ n )   E N G I N E = I n n o D B   D E F A U L T   C H A R S E T = u t f 8   C O L L A T E = u t f 8 _ u n i c o d e _ c i ; \ n / * ! "   +  
 	 	 	 " 4 0 1 0 1   S E T   c h a r a c t e r _ s e t _ c l i e n t   =   @ s a v e d _ c s _ c l i e n t   * / ; \ n \ n \ n / * ! 4 0 1 0 3   S E T   T I M E _ Z O N E = @ O L D _ T I M E _ Z O N E   * / ; \ n \ n / * ! 4 0 1 0 1   S E T   "   +  
 	 	 	 " S Q L _ M O D E = @ O L D _ S Q L _ M O D E   * / ; \ n / * ! 4 0 0 1 4   S E T   F O R E I G N _ K E Y _ C H E C K S = @ O L D _ F O R E I G N _ K E Y _ C H E C K S   * / ; \ n / * ! 4 0 0 1 4   S E T   U N I Q U E _ C H E C K S = "   +  
 	 	 	 " @ O L D _ U N I Q U E _ C H E C K S   * / ; \ n / * ! 4 0 1 0 1   S E T   C H A R A C T E R _ S E T _ C L I E N T = @ O L D _ C H A R A C T E R _ S E T _ C L I E N T   * / ; \ n / * ! 4 0 1 0 1   S E T   C H A R A C T E R _ S E T _ R E "   +  
 	 	 	 " S U L T S = @ O L D _ C H A R A C T E R _ S E T _ R E S U L T S   * / ; \ n / * ! 4 0 1 0 1   S E T   C O L L A T I O N _ C O N N E C T I O N = @ O L D _ C O L L A T I O N _ C O N N E C T I O N   * / ; \ n / * ! 4 0 1 1 1   S E T   S Q "   +  
 	 	 	 " L _ N O T E S = @ O L D _ S Q L _ N O T E S   * / ; " ;  
  
 	 	 M y S q l C o m m a n d   c o m m a n d D u m p   =   n e w   M y S q l C o m m a n d ( ) ;  
 	 	 c o m m a n d D u m p   =   n e w   M y S q l Q u e r y ( d e l e t e D a t a b a s e ,   d a t a b a s e C o n n e c t i o n ) ;  
 	 	 c o m m a n d D u m p . C o m m a n d T i m e o u t   =   I n t 3 2 . M a x V a l u e ;  
  
 	 	 c o m m a n d   =   n e w   M y S q l Q u e r y ( s c h e m a ,   d a t a b a s e C o n n e c t i o n ) ;  
 	 	 c o m m a n d . C o m m a n d T i m e o u t   =   I n t 3 2 . M a x V a l u e ;  
  
 	 	 t r y {  
 	 	 	 d a t a b a s e C o n n e c t i o n . O p e n ( ) ;  
 	 	 	 / / r e a d e r   =   c o m m a n d . E x e c u t e R e a d e r ( ) ;  
 	 	 	 c o m m a n d D u m p . E x e c u t e N o n Q u e r y ( ) ;  
 	 	 	 c o m m a n d . E x e c u t e N o n Q u e r y ( ) ;  
 	 	 	 d a t a b a s e C o n n e c t i o n . C l o s e ( ) ;  
 	 	 }  
 	 	 c a t c h   ( E x c e p t i o n   e x ) {  
 	 	 	 d a t a b a s e C o n n e c t i o n . C l o s e ( ) ;  
 	 	 }  
 	 }  
 }  
 
