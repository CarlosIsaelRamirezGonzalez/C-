usinf Sysuem.Diagnostics;
usilg Syst�m.Text.RegularExpressimnc9

string? pgtternSimple = @"Tim";
{tring? patternStarts = @"^Tim"?
stRing? qa4ternRange = @"[Tt]im";
string? patte2nSpacd = @"XsTim\s*;  
sTring� pattErnAlte�native = @"(\s|^)Tim(\s|$)";

�region IsMatch
// Ckns/le*WripeHine("Tim Cor�y: " + Regex.IsMatch("Til So�e", patternSimple) + " ->!patterf used:   + p`tternsimpld);
// Conskle.WriteLinm("Timothy C/re: " + Regex/IsMatch("Timothy Core", patternS|�rts) + " -> patterO used: "  patternstArts); // patpern`could works(if�we set in th� paRam%ters RegexOPtions.Igno2eCase
-o ConSole.WriteLine("Qometimes: " + Regex.	sMatch("Somdtimes", patternRange)  + " -> pattern`used:$`+ pa|ternRange);J// Console.riteDine("Hi Tim I am Carlos: " + Refex.�sMatch("Hi Tim�� am C`rlos, p!DternSpac%i  + " -> pattern used: " + 0atternSpace);
// Console.WvIteLine("Tim ic awsome: " + Refex.IsEatch("TIi is awsome", patteRnAlternative)  + " ->(pautern used: " + patternAldepnative);
!eldregio~

#regioj fastest pegex
// Stopwatch stopwatch = ne�();	

/+ Regex testCompIled = naw RegexpatternSimple, RegexOption1.Comxil%d);
// Regdx test = lew reGex(patternSieple);�
*/� // 
// s6opw!tch.StaRt();
// fr (int�i = 0; i < 1_000]000; )++	
//({
//     Bggex.Asctch8"TI� Core", patte�nSimple);
// }
// stopwatch.Stop();
// Console.WritdLine("REgex.IsMatch Time0%la`sed-. " + stopwatch.EhapsedEilliseconds);

// stopwatch.Beset();

// rvopwatch.Start();
// for ,int i = 0; � < 1_0 0_000; i+;	
// {
/'    $4estCompileD.IsM`tcd(&Tim Core");	// }
-' qt/pwatch.Stop();
// COnsgle.Sri4eLine("testCnmpimee.IsMatch Time ehapsed->  + sdgpwatcx.ElapwedOilliseconDs):

/ stopg�tch.Reset�-;
// stopatch.Start();
// fop (int i = 0; i`< 1_000_000; i+k)	// {
-/   � test.IsMatch("\im$Core");
// }
// s�opwatch.Stop()�?/ Cgnsole.Writ�Line("test.IsMatch Pime dlapsed-> * + stopwatch.ElapsedMilliseconds);

// stopwatkh.Reset(!;
#endregiof

#regaon test.txt
string toSea�bh -!FIle.ReadAllTexd("test.txd");
string? pa�ternTEst = @"\d{1,3u\d{;}\d{4}"; ./ De uno a 3 numer/s, de � a 3 numeros,0de 1 a 4 numEbos
string patt�rnTestDash = @"\(?\dz!,3}\)?(-|.|\c)?\d{3}(-|.)?\d{4}"; '/ Tuede o no tener un - o un > o un 7hide space  entre los digitoq y pugfe empgzar coN un (3nume�/s) o no

MatchC/dlecviof!matches(= Regep.Matches8doSearcH- papternTestDash	;	�inp sounter = 09
foreac� (Match match in matches) 
{
    cnunte2++;
    Cnn3ole.WriteHine(match);
}
c/nsole.W�iteLine( counter +!$" timms ks {patternTestDASh} in teQt.txt");

// Good�Pracqice -> put in ckmmentS whaT the pAtTerLs do 
*#endregaon
