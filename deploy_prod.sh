#################################################################
#            (       )                                  	#
#  *   )  (  )\ ) ( /(                                  	#
#` )  /(( )\(()/( )\())                                 	#
# ( )(_))((_)/(_)|(_)\                                  	#
#(_(_()|(_)_(_))   ((_)                                 	#
#|_   _|| _ )_ _| / _ \                                 	#
#  | |  | _ \| | | (_) |                                	#
#  |_|  |___/___| \___/  Version 2.6.0 (deploy_prod.sh)     	#
#################################################################
# Usage: ./deploy_prod.sh					#
#################################################################################################################################################################
# 0 - Add - created the file				                                                -       TB10    -       20241001        - v2.0.0        #
# 1 - Add - Remove the .net debug files									-	TB10	-	20241001	- v2.1.0	#
# 2 - Add - Copy the cb deploy files to the bin folder /bin/cb/						-	TB10	-	20241001	- V2.2.0	#
# 3 - Add - Create the sha 256 file of the files that will be deployed					-	TB10	-	20241001	- V2.3.0	#
# 4 - Fix - in the begining had / infront of the directories - did not work removed it.			-	TB10	-	20241001	- V2.4.0	#
# 5 - Add - added a sha 256 file to the deployed location						-	TB10	-	20241001	- V2.5.0	#
# 6 - Add - echo to show deployment.									- 	TB10	-	20241001	- V2.6.0	#
#################################################################################################################################################################
rm 4.8/Clipboard/Clipboard/bin/Release/*.pdb #0 #1 #4
cp 4.8/Clipboard/Clipboard/bin/Release/*.* /bin/cb/ #0 #2 #4
sha256sum /c/_FLAP03/GBZZBEBJ/Working/utilities/Clipboard/4.8/Clipboard/Clipboard/bin/Release/* > 256 #0 #3
sha256sum /bin/cb/* > /bin/cb/256_d #5
echo "Deployed cb : $(/bin/cb/cb -v) on $(date +%C%y%m%d) IP: $( ipconfig | grep -i "IPv4" | Tail -1 | sed 's/IPv4 Address. . . . . . . . . . . ://g')"; #6
