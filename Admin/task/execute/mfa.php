<?php
    $curl = curl_init();
    curl_setopt($curl, CURLOPT_URL, "https://www.portal.dlsbiotech.com/admin/task/execute/mfa.aspx?id=mfa");
    curl_setopt($curl,CURLOPT_USERAGENT,"useragentvalue");
       curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
    curl_exec($curl);
    curl_close($curl);
?>
