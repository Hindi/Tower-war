<?PHP

$id = $_POST['id'];	
$action = $_POST['action'];
$friendId = $_POST['friendId'];

$con = mysql_connect("mysql51-140.perso","vstudermod1","n2aHZEKG27zR") or ("Cannot connect!"  . mysql_error());
if (!$con)
	die('Could not connect: ' . mysql_error());
  
mysql_select_db("vstudermod1" , $con) or die ("could not load the database" . mysql_error());


function confirm($arg_1, $arg_2)
{
  $ins = mysql_query("REPLACE INTO  `tw_relationships` (  `id` , `date` ) VALUES ('.$curId.' ,  '') ; ");
  $curId = mysql_insert_id();
  $ins = mysql_query("REPLACE INTO  `tw_user_relationships` (  `id` , `userId1`, `userId2` ) VALUES ('.$curId.' ,  '".$arg_1."',  '".$arg_2."') ; ");
  $ins = mysql_query("DELETE FROM  tw_relationships_request WHERE targetId = '".$arg_2."' AND askerId = '".$arg_1."'; ");
	if ($ins)
		die ("0");
	else
		die ("Error: " . mysql_error());
}

if($action == "confirm")
{ 
  $res2 = mysql_query("SELECT id FROM `tw_relationships_request` WHERE targetId ='".$friendId."' AND askerId = '".$id."'; ");  //The future friend requested for a relationship
  if(mysql_num_rows($res2) > 0)
  {
    confirm($id, $friendId);
  }
	else
		die ("Error: There is no existing request to confirm.");
}
else if($action == "refuse")
{
  $ins = mysql_query("DELETE FROM  tw_relationships_request WHERE targetId = '$id';");
	if ($ins)
		die ("0");
	else
		die ("Error: " . mysql_error());
}
else if($action == "delete")
{
  $ins = mysql_query("DELETE FROM  tw_relationships WHERE id = '".$id."';");
  $ins = mysql_query("DELETE FROM  tw_user_relationships WHERE id = '".$id."';");
	if ($ins)
		die ("0");
	else
		die ("Error: " . mysql_error());
}
else if($action == "request")
{
  $res = mysql_query("SELECT id FROM `tw_relationships_request` WHERE `targetId`='".$id."' AND `askerId`='".$friendId."' ; ");
  if(mysql_num_rows($res) == 0)
  {
    $rqId = $id * $friendId;
    $ins = mysql_query("REPLACE INTO  `tw_relationships_request` ( `id` , `askerId` , `targetId` ) VALUES ('.$rqId.', '.$id.' ,  '.$friendId.') ; ");
	  if ($ins)
		  die ("0");
	  else
		  die ("Error: " . mysql_error());
  }
  else
  {
    confirm($id, $friendId);
  }
}
else if($action == "list")
{
  $result1 = mysql_query(" SELECT userId1 FROM `tw_user_relationships` WHERE `userId2` = '".$id."'");
  $result2 = mysql_query(" SELECT userId2 FROM `tw_user_relationships` WHERE `userId1` = '".$id."'");
  
  $answer = "";
  while ($row = mysql_fetch_array($result1, MYSQL_NUM)) {
    $answer = $answer . $row[0] . ",";
  } 
  while ($row = mysql_fetch_array($result2, MYSQL_NUM)) {
    $answer = $answer . $row[0] . ",";
  } 
  die($answer);
}
?>