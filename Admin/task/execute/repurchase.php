<?php
    $curl = curl_init();
    curl_setopt($curl, CURLOPT_URL, "https://mysirisaurabha.com/admin/task/execute/repurchaseBv.aspx?id=monthly");
    curl_setopt($curl,CURLOPT_USERAGENT,"useragentvalue");
   
        curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
         curl_exec($curl);
    curl_close($curl);
?>
