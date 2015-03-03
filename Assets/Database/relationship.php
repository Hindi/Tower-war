<?PHP

$id = $_POST['id'];	//Either the relationship's or the asking user's id
$action = $_POST['action'];
$friendId = $_POST['friendId'];

$con = mysql_connect("mysql51-140.perso","vstudermod1","n2aHZEKG27zR") or ("Cannot connect!"  . mysql_error());
if (!$con)
	die('Could not connect: ' . mysql_error());
	
mysql_select_db("vstudermod1" , $con) or die ("could not load the database" . mysql_error());

if($action == "confirm")
{
  //Here id must be the relationship's id
  $ins = mysql_query("REPLACE INTO  `tw_relationships` (  `id` , `date` ) VALUES ('.$curId.' ,  '') ; ");	//Add an entry in the relationship table
  $curId = mysql_insert_id()
  $ins = mysql_query("REPLACE INTO  `tw_user_relationships` (  `id` , `userId` ) VALUES ('.$curId.' ,  '".$friendId."') ; ");	//Add the relation between the relationship and the two users
  $ins = mysql_query("REPLACE INTO  `tw_user_relationships` (  `id` , `userId` ) VALUES ('.$curId.' ,  '".$id."') ; ");
	if ($ins)
		die ("0");
	else
		die ("Error: " . mysql_error());
}
if($action == "refuse")
{
  $ins = mysql_query("DELETE FROM  tw_relationships_request WHERE targetId = '$id';");	//Delete the request
	if ($ins)
		die ("0");
	else
		die ("Error: " . mysql_error());
}
else if($action == "delete")
{
  //Here id must be the relationship's id
  $ins = mysql_query("DELETE FROM  tw_relationships WHERE id = '".$id."';");		//Delete the relationship
  $ins = mysql_query("DELETE FROM  tw_user_relationships WHERE id = '".$id."';");	//Delete the relationship
	if ($ins)
		die ("0");
	else
		die ("Error: " . mysql_error());
}
else if($action == "ask")
{
  //Here id must be the asker's id
  $ins = mysql_query("REPLACE INTO  `tw_relationships_request` (  `askerId` , `targetId` ) VALUES ('.$id.' ,  '.$friendId.') ; ");	//Add an entry in the relationship request table
	if ($ins)
		die ("0");
	else
		die ("Error: " . mysql_error());
}
else if($action == "list")
{
  //Here id must be the asker's id
  $check = mysql_query("SELECT a.userId, b.userId AS userId
						FROM tw_user_relationships a 
						INNER JOIN tw_user_relationships b
						ON a.id = b.id
						WHERE a.userId != '.$id.'
						AND b.userId = '.$id.'");
  $numrows = mysql_num_rows($check);
  if ($numrows == 0)
  {
	  die ("id does not exists \n");
  }
  else
  {
    $row = mysql_fetch_assoc($check);
	  die($row['level']);
  }
}



?>