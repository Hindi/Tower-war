<?PHP

$user = $_POST['user'];
$pass = $_POST['password'];

$con = mysql_connect("mysql51-140.perso","vstudermod1","n2aHZEKG27zR") or ("Cannot connect!"  . mysql_error());
if (!$con)
	die('Could not connect: ' . mysql_error());
	
mysql_select_db("vstudermod1" , $con) or die ("could not load the database" . mysql_error());

$check = mysql_query("SELECT * FROM tw_users WHERE `login`='".$user."'");
$numrows = mysql_num_rows($check);
if ($numrows == 0)
{
	$pass = md5($pass);
	$ins = mysql_query("INSERT INTO  `tw_users` (  `id` ,  `login` ,  `pass` ) VALUES ('' ,  '".$user."' ,  '".$pass."') ; ");
	if ($ins)
		die ("Succesfully Created User!");
	else
		die ("Error: " . mysql_error());
}
else
{
	die("User allready exists!");
}


?>