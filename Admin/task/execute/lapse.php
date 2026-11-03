<?php
    $curl = curl_init();
    curl_setopt($curl, CURLOPT_URL, "http://goldentreeonline.com/admin/task/execute/lapse.aspx?id=lapse");
    curl_setopt($curl,CURLOPT_USERAGENT,"useragentvalue");
    curl_exec($curl);
    curl_close($curl);
?>
