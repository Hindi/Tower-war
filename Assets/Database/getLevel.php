<?PHP

$id = $_POST['id'];
$action = $_POST['action'];
$level = $_POST['level'];

$con = mysql_connect("mysql51-140.perso","vstudermod1","n2aHZEKG27zR") or ("Cannot connect!"  . mysql_error());
if (!$con)
	die('Could not connect: ' . mysql_error());
	
mysql_select_db("vstudermod1" , $con) or die ("could not load the database" . mysql_error());

if($action == "set")
{
  $ins = mysql_query("REPLACE INTO  `tw_levels` (  `id` , `level` ) VALUES ('.$id.' ,  '".$level."') ; ");
	if ($ins)
		die ("0");
	else
		die ("Error: " . mysql_error());
}
else
{
  $check = mysql_query("SELECT level FROM tw_levels WHERE `id`='".$id."'");
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