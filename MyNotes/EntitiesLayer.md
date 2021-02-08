# Entities Katmanı

Entities katmanı: bizim tüm domain'imize hizmet eden objeleri içerir.  (yardımcı katman gibi düşünebilirsiniz.)

Basit bir obje ile başlamıştık ;  Product ve Category ile.

 - Category de kategorinin ismi ve id'si
 
- Product'da 5 özellik vererek bu süreci tamamladk.

 - **Herhangi bir class çıplak kalmasın**   kurali ile Category veya Product nesnesinin bir VT nesnesi olduğunu anlatmak üzere bir imzalama gerçekleştirdik. 
 
- Bu imzalama  **`IEntity`** ile yapıldı.   
	
	:yum:Bu tamamen mimariyi yazan kişinin yoğurt yiyişidir... farklı şekillerde İnterface imzaları olabilir.(base tip gibi..)