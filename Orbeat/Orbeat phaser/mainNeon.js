var game = new Phaser.Game(740, 960, Phaser.CANVAS, 'Orbeat');
var introBeat,titleImpact,titleImpact2,orbeat;
var fullscreenText;
var xarr=[];
var emitter1,emitter2,emitter3;

var playState={    
    create:function () {
        SetEnvironment();
        var style = { font:"100px myFirstFont", fill: "#0061ff", align: "center" };
        var title = game.add.text(game.world.centerX, game.world.centerY-300, "ORBEAT", style);
        title.anchor.set(0.5);
        var style = { font:"100px myFirstFont", fill: "#ffffff", align: "center" };
        var title2 = game.add.text(game.world.centerX, game.world.centerY-300, "ORBEAT", style);
        title2.anchor.set(0.5);
        
        InitializePlayerOrbits();
        InitializeGui();
		
        var k=1;
        for (var i = 0; i < 20; i++) {
        	xarr[i]=k;
        	k++;
        }
        shuffle(xarr);
        InitializeGameColorsForTitle(xarr[0]);
        setGameColorsForTitle();
        introBeat = game.add.audio('introBeat');

        for (var i = 0; i < 3; i++) {
        	orbit.children[i].scale.set(11);
        }
        title.scale.set(0);
        title2.scale.set(0);
		titleImpact = game.add.audio('titleImpact');
		titleImpact2 = game.add.audio('titleImpact');
		orbeat = game.add.audio('orbeatVoice');
        game.add.tween(orbit.children[2].scale).to({x:1.05,y:1.05},500,"Cubic",true).onComplete.add(
        	function next1(){
        		game.camera.shake(0.025,200);

                titleImpact2.play('',0,1);
				game.add.tween(orbit.children[1].scale).to({x:1.05,y:1.05},500,"Cubic",true).onComplete.add(
        			function next2(){
       	        		game.camera.shake(0.025,200);

       		            titleImpact2.play('',0,1);
						game.add.tween(orbit.children[0].scale).to({x:1.05,y:1.05},500,"Cubic",true).onComplete.add(
        					function titleNext(){
        						game.camera.shake(0.025,200);
						        titleImpact2.play('',0,1);
								game.add.tween(title.scale).to({x:1.05,y:1.05},500,"Linear",true);
        						game.add.tween(title2.scale).to({x:1.05,y:1.05},500,"Linear",true).onComplete.add(
        							function titleShake(){
        								game.add.tween(title.scale).to({x:1,y:1},200,"Cubic",true,0,-1,true);
        								game.add.tween(title2.scale).to({x:1,y:1},200,"Cubic",true,0,-1,true);
        								game.add.tween(title2).to({alpha:0},500,"Cubic",true,0,-1,true);    
        								game.add.tween(playBtn).to({alpha:1},500,"Cubic",true,0,-1,true);
										game.add.tween(orbit.children[0].scale).to({x:1,y:1},200,"Cubic",true,0,-1,true);
										game.add.tween(orbit.children[1].scale).to({x:1,y:1},200,"Cubic",true,0,-1,true);
										game.add.tween(orbit.children[2].scale).to({x:1,y:1},200,"Cubic",true,0,-1,true);
									
									    game.camera.flash(0xffffff, 400);
									    game.time.events.add(250,playOrbeat);

        								introBeat.play('',0,1,true);
										playBtn.inputEnabled=true;
								        playBtn.visible=true;
								        playBtn.events.onInputDown.add(GameStart, this);
        							});
        					});
        			});
        	});

        fullscreenText = game.add.image(game.world.width-50, 50, 'fullscreenBtn');
        fullscreenText.anchor.set(0.5);
        fullscreenText.scale.set(0.25);
        fullscreenText.inputEnabled=true;
        fullscreenText.visible=true;
        fullscreenText.events.onInputDown.add(gofull);//for fullscreen


        spaceBar = game.input.keyboard.addKey(Phaser.Keyboard.SPACEBAR);
    },
    update:function () {
        orbit.children[2].rotation+=0.05;
        orbit.children[1].rotation-=0.05;
	    spaceBar.onDown.addOnce(GameStart);//for keyboard
    }
}
function playOrbeat(){
	orbeat.play('',0,1);
}
function InitializeGameColorsForTitle(num){

    switch (num) {
        //NEON COLORS
        case 1:{
			bgColor = 0x000000;
	        orbitColor = 0xff0000;
	        playBtnColor = 0xff0000;
            break;
        }
        case 2:{
        	bgColor = 0x000000;
	        orbitColor = 0xffd001;
	        playBtnColor = 0xd0ff00;
            break;
        }
        case 3:{
        	bgColor = 0x000000;
	        orbitColor = 0xcfff00;
	        playBtnColor = 0xcfff00;
            break;
        }
        case 4:{
        	bgColor = 0x000000;
	        orbitColor = 0x5aff00;
	        playBtnColor = 0x74ed9a;
            break;
        }
        case 5:{
        	bgColor = 0x000000;
			orbitColor = 0x00ff9c;
	        playBtnColor = 0x00ff9c;
            break;
        }
        case 6:{
        	bgColor = 0x000000;
			orbitColor = 0x009dff;
	        playBtnColor = 0x009dff;
            break;
        }
        case 7:{
        	bgColor = 0x000000;
			orbitColor = 0x010eff;
	        playBtnColor = 0x010eff;
            break;
        }
        case 8: {
            bgColor = 0x000000;
            orbitColor = 0x010eff;
            playBtnColor = 0x010eff;
            break;

        }
        case 9: {
            bgColor = 0x000000;
            orbitColor = 0x8900f9;
            playBtnColor = 0x8900f9;

            break;

        }
        case 10: {
            bgColor = 0x000000;
            orbitColor = 0xf800ff;
            playBtnColor=0xf800ff;

            break;

        }
        case 11: {
        	bgColor = 0x000000;
			orbitColor = 0xff008f;
            playBtnColor = 0xff008f;
            break;

        }
        case 12: {
        	bgColor = 0x000000;
	        orbitColor = 0xff008f;
            playBtnColor = 0xff008f;
            break;

        }
        case 13: {
        	bgColor = 0x000000;
	        orbitColor = 0xff0050;
            playBtnColor = 0xf93674;
            break;

        }
        case 14: {
			bgColor = 0x000000;
	        orbitColor = 0xfc3a3a;
            playBtnColor = 0xfc3a3a;
            break;

        }
        case 15: {
            bgColor = 0x000000;
            orbitColor = 0xffffff;
            playBtnColor=0xff0000;
            break;

        }
        case 16: {
            bgColor = 0x000000;
            orbitColor = 0xff0000;
            playBtnColor=0xffffff;
           break;

        }
        case 17: {
			bgColor = 0x000000;
	        orbitColor = 0xffe900;
            playBtnColor = 0x1900ff;            
           break;

        }
        case 18: {
       		bgColor = 0x000000;
	        orbitColor = 0x7605ff;
            playBtnColor = 0xffbc05;
            break;

        }
        case 19: {
            bgColor = 0x000000;
	        orbitColor = 0xff00ff;
            playBtnColor = 0xfbff00;
            break;

        }
        case 20: {
           	bgColor = 0x000000;
	        orbitColor = 0xff0217;
            playBtnColor = 0x17ff02;
            break;

        }
          
    }
}
function setGameColorsForTitle(){
    game.stage.backgroundColor=bgColor;
    playBtn.tint=playBtnColor;    
    for(var i=0;i<3;i++){
        if(i!=1)
            orbit.children[i].tint=orbitColor;
    }
    orbit.children[1].tint=0xff0000;    
}
function GameStart(){	
    game.state.start("mainState");
}
var hurdle_blast,player_blast,perfecthit,fireLaunch,beat;
var letsgoVoice,gameoverVoice,perfecthitVoice,awesomeVoice,bullseyeVoice,smashingVoice,deadcenterVoice;
var mainState={
    create:function () {
        introBeat.stop();
        SetEnvironment();
        JSONLoading();
        LoadAllVariables();
        game.physics.startSystem(Phaser.Physics.ARCADE);       
        InitializeBackgroundOrbits();
        InitializeGui();
        InitializeShareBtn();
        InitializePlayerOrbits();
        InitializePlayer();
        InitializeFireKey();
        InitializeHurdle();
        InitializeScore();
        InitializeHighScore();
        setColorsArray();
        InitializeGameColors(colorsArray[0]);
        InitializePerfectHitScore();
        setGameColors();
        InitializeWarning();
        StartBeats();
        hurdle_blast = game.add.audio('hurdle_blast');
        player_blast = game.add.audio('player_blast');
        perfecthit = game.add.audio('perfecthit');
        fireLaunch = game.add.audio('fireLaunch');

       	letsgoVoice = game.add.audio('letsgoVoice');
       	gameoverVoice = game.add.audio('gameoverVoice');
       	perfecthitVoice = game.add.audio('perfecthitVoice');
       	awesomeVoice = game.add.audio('awesomeVoice');
       	bullseyeVoice = game.add.audio('bullseyeVoice');
       	smashingVoice = game.add.audio('smashingVoice');
       	deadcenterVoice = game.add.audio('deadcenterVoice');
       	

        beat = game.add.audio('beat');
        game.time.events.add(750,BGMusic,this);        //to perfectly sync with bgOrbitBeat
    },
    update:function () {
    RotateOrbits();
    	if(gameoverFlag==false){

    		CheckGUI();
	        RotatePlayerOnOrbit();
	        GiveWarning();

	        ReduceOrbitWithTime();
	        DestroyPlayerWithTime();
	        RotateHurdle();

	        GameControls();
	        game.physics.arcade.collide(player, hurdle,hitHurdle);
	    }
	    else if(gameoverFlag==true){
	    	spaceBar.onDown.removeAll();
	    	spaceBar.onDown.addOnce(RestartGame);

	    }
    },
    render:function () {        
        // game.debug.text(game.time.fps,20,20);
        // game.debug.text(clicked,20,20);

    }
}

//******************************Game Screen Functions
function CheckGUI(){
	highscoreText.alpha=0;
	playBtn.alpha=0;
	shareBtn.alpha=0;
    shareBtn.inputEnabled=false;
}

var fullScreen;
function SetEnvironment() {
    game.time.advancedTiming = true;//for fps
    game.scale.pageAlignHorizontally=true;
    game.scale.pageAlignVertically=true;
    game.scale.scaleMode=Phaser.ScaleManager.SHOW_ALL;
    game.scale.fullScreenScaleMode = Phaser.ScaleManager.SHOW_ALL;
    fullScreen = game.input.keyboard.addKey(Phaser.Keyboard.F);
}
function gofull() {

    if (game.scale.isFullScreen)
    {
        game.scale.stopFullScreen();
    }
    else
    {
        game.scale.startFullScreen();
    }

}
function BGMusic(){	
   	beat.play('',0,1,true);
}
function GameControls(){
	if(gameoverFlag==false){
	    fullScreen.onDown.addOnce(gofull);//for fullscreen
	    if(playerEnterFromCenterFlag==true){
	    	spaceBar.onDown.removeAll();
    	    spaceBar.onDown.addOnce(fire);//for keyboard	            
    	    if(game.input.activePointer.isDown && isFired==false){
    	        fire();
    	    }
    	    else{
    	        game.input.activePointer.reset();
    	    }
        }
	}
}

//******************************JSON functions
var phaserJSON;
function JSONLoading(){
    phaserJSON = game.cache.getJSON('myjson');
}
function LoadAllVariables() {
    minRevolveSpeed = phaserJSON.minRevolveSpeed;
    maxRevolveSpeed = phaserJSON.maxRevolveSpeed;
    shootingSpeed = phaserJSON.shootingSpeed;
    hurdle_x1=phaserJSON.hurdle_x1;
    hurdle_x2=phaserJSON.hurdle_x2;
    hurdle_y1=phaserJSON.hurdle_y1;
    hurdle_y2=phaserJSON.hurdle_y2;
    beatSpeed=phaserJSON.beatSpeed;
    beatFrequency=phaserJSON.beatFrequency;
    transitionSpeed=phaserJSON.transitionSpeed;
    playerZoomSpeed=phaserJSON.playerZoomSpeed;
    scorebeatSpeed=phaserJSON.scorebeatSpeed;
}

//******************************Initializations
var playBtn;
function InitializeGui(){
	playBtn=game.add.image(game.world.centerX+5,game.world.centerY,'playBtn');
	playBtn.anchor.set(0.5);
    playBtn.visible=false;
    playBtn.scale.set(0.7);
    playBtn.alpha=0;
}
var shareBtn;
function InitializeShareBtn(){
    shareBtn = game.add.image(game.world.centerX,game.world.centerY,"shareBtn");
    shareBtn.anchor.set(0.5);
    shareBtn.scale.set(0);
    shareBtn.tint=0xffffff;
    shareBtn.events.onInputDown.add(shareScore);
    shareBtn.inputEnabled=false;
    shareBtn.alpha=0;
}

var player;
var rotatePlayer;//flag
var playerRevolveSpeed;//player's revolving speed
var minRevolveSpeed,maxRevolveSpeed;//player's revolving speed range
var shootingSpeed;//player's shooting speed
var playerOrbitPosition=130;
var playerZoomSpeed;
var arrow_head;
function InitializePlayer(){
    player = game.add.sprite(game.world.centerX, game.world.centerY, 'player');
    arrow_head = game.add.sprite(-20, -20, 'arrow_head');
    arrow_head.angle=player.angle-7;
    game.physics.enable(player, Phaser.Physics.ARCADE);
    player.anchor.setTo(0.5);
    arrow_head.anchor.set(0.5);

    player.addChild(arrow_head);

    SetCircleCollider(player);
    setPlayerOrbitPosition(playerOrbitPosition);
    setPlayerRandomRevolveSpeed();
    rotatePlayer=true;
    setPlayerRandomDirection();

    player.body.collideWorldBounds = true;
    player.body.onWorldBounds = new Phaser.Signal();
    player.body.onWorldBounds.add(hitWorldBounds);
    
    player.visible=false;
}
var playerEnterFromCenterFlag=false;
function playerEnterFromCenter(){
	player.visible=true;
	game.add.tween(player.scale).from({x:0,y:0},playerZoomSpeed,"Linear",true);
    game.add.tween(player.scale).to({x:1,y:1},playerZoomSpeed,"Linear",true).onComplete.add(AllowPlayerFire);
}
var orbit;
function InitializePlayerOrbits(){
    orbit=game.add.group();
    for (var i=1;i<=3;i++){
        orbit.create(game.world.centerX,game.world.centerY,'orbit'+i);
    }
    orbit.setAllChildren('anchor.x',0.5);
    orbit.setAllChildren('anchor.y',0.5);

    orbit.setAllChildren('scale.x',1.1);
    orbit.setAllChildren('scale.y',1.1);
}
var spaceBar;
function InitializeFireKey(){
    spaceBar = game.input.keyboard.addKey(Phaser.Keyboard.SPACEBAR);
}
var score=0;
var scoreText;
function InitializeScore(){
    var style = { font:"70px myFirstFont", fill: "#ffffff", align: "center" };
    scoreText = game.add.text(game.world.centerX, game.world.centerY, String(score), style);
    scoreText.anchor.set(0.5);
    scoreText.alpha=0.25;
    scoreText.visible=false;
}
var highscore;
var highscoreText;
function InitializeHighScore(){
	if (localStorage.getItem("orbitBeatHighScore") == null){    	
		localStorage.setItem("orbitBeatHighScore",0);
	}    
	highscore = parseInt(localStorage.getItem("orbitBeatHighScore"));
	var style = { font:"70px myFirstFont", fill: "#ffffff", align: "center" };
	highscoreText = game.add.text(game.world.centerX, game.world.centerY, highscore.toString(), style);
	highscoreText.anchor.set(0.5);
	highscoreText.alpha=0;
}

var perfectHitText;
function InitializePerfectHitScore(){
    var style = { font:"70px myFirstFont", fill: "#ffcc00", align: "center" };
    perfectHitText = game.add.text(game.world.centerX, game.world.centerY-200, "", style);
    perfectHitText.anchor.set(0.5);
    perfectHitText.alpha=0;
}
var hurdle,hurdleOrbit;
var hurdle_x1,hurdle_x2,hurdle_y1,hurdle_y2;
var levelCount;
function InitializeHurdle(){
    levelCount=0;
    hurdle=game.add.sprite(game.world.centerX,game.world.centerY,'hurdle');
    hurdleOrbit=game.add.sprite(0,0,'hurdleOrbit');
    game.physics.enable(hurdle, Phaser.Physics.ARCADE);
    hurdle.anchor.set(0.5);
    hurdleOrbit.anchor.set(0.5);
    hurdle.addChild(hurdleOrbit);

    SetCircleCollider(hurdle);
    setHurdleOrbitPosition();
    hurdle.visible=false;
    hurdleOrbit.visible=false;
    hurdle.body.enable=false;


}
function hurdleZoomToPosition(){
	hurdle.visible=true;
    hurdleOrbit.visible=true;
	game.add.tween(hurdle.scale).from({x:0,y:0},transitionSpeed,Phaser.Easing.Linear.Out,true);
    game.add.tween(hurdle.scale).to({x:1,y:1},transitionSpeed,Phaser.Easing.Linear.Out,true);
    hurdle.body.enable=true;
}
var bgColor,orbitColor,orbitColorWarning,hurdleColor,hurdleOrbitColor,playerColor,arrow_headColor,playBtnColor,shareBtnColor,scoreTextColor,highscoreTextColor;
function InitializeGameColors(num){
    switch (num){
    	case 1:{
    		bgColor = 0x000000;
			orbitColor = 0xff0000;
			hurdleColor = 0xff8300;
			hurdleOrbitColor = 0xff8300;
			playerColor = 0xff8300;
			arrow_headColor = 0xff8300;	        
			scoreTextColor=0xff8300;			
	        playBtnColor = 0xff8300;
	        shareBtnColor = 0xff8300;
            highscoreTextColor=0xff8300;
            break;
        }
        case 2:{
    		bgColor = 0x000000;
			orbitColor = 0xffd001;
	        hurdleColor = 0xd0ff00;
	        hurdleOrbitColor = 0xd0ff00;
	        playerColor = 0xd0ff00;
	        scoreTextColor=0xd0ff00;
			arrow_headColor = 0xd0ff00;	        
	        playBtnColor = 0xd0ff00;
	        shareBtnColor = 0xd0ff00;
            highscoreTextColor=0xd0ff00;
            break;
        }
        case 3:{
    		bgColor = 0x000000;
			orbitColor = 0xcfff00;
	        hurdleColor = 0xcfff00;
	        hurdleOrbitColor = 0xcfff00;
	        playerColor = 0xcfff00;
	        scoreTextColor=0xcfff00;
			arrow_headColor = 0xcfff00;	        
	        shareBtnColor = 0xcfff00;
	        playBtnColor = 0xcfff00;
            highscoreTextColor=0xcfff00;
            break;
        }
        case 4:{
    		bgColor = 0x000000;
			orbitColor = 0x5aff00;
	        hurdleColor = 0x74ed9a;
	        hurdleOrbitColor = 0x74ed9a;
	        playerColor = 0x74ed9a;
	        scoreTextColor=0x74ed9a;
			arrow_headColor = 0x74ed9a;	        
	        shareBtnColor = 0x74ed9a;
	        playBtnColor = 0x74ed9a;
            highscoreTextColor=0x74ed9a;
            break;
        }
        case 5:{
    		bgColor = 0x000000;
			orbitColor = 0x00ff9c;
	        hurdleColor = 0x00ff9c;
	        hurdleOrbitColor = 0x00ff9c;
	        playerColor = 0x00ff9c;
	        scoreTextColor=0x00ff9c;
			arrow_headColor = 0x00ff9c;	        
	        shareBtnColor = 0x00ff9c;
	        playBtnColor = 0x00ff9c;
            highscoreTextColor=0x00ff9c;
            break;
        }
        case 6:{
    		bgColor = 0x000000;
			orbitColor = 0x009dff;
	        hurdleColor = 0x009dff;
	        hurdleOrbitColor = 0x009dff;
	        playerColor = 0x009dff;
	        scoreTextColor=0x009dff;
			arrow_headColor = 0x009dff;	        
	        shareBtnColor = 0x009dff;
	        playBtnColor = 0x009dff;
            highscoreTextColor=0x009dff;
            break;
        }
        case 7:{
    		bgColor = 0x000000;
			orbitColor = 0x8900f9;
	        hurdleColor = 0x8900f9;
	        hurdleOrbitColor = 0x8900f9;
	        playerColor = 0x8900f9;
	        scoreTextColor=0x8900f9;
			arrow_headColor = 0x8900f9;	        
	        shareBtnColor = 0x8900f9;
	        playBtnColor = 0x8900f9;
            highscoreTextColor=0x8900f9;
            break;
        }
        case 8:{
    		bgColor = 0x000000;
			orbitColor = 0x010eff;
	        hurdleColor = 0x010eff;
	        hurdleOrbitColor = 0x010eff;
	        playerColor = 0x010eff;
	        scoreTextColor=0x010eff;
			arrow_headColor = 0x010eff;	        
	        shareBtnColor = 0x010eff;
	        playBtnColor = 0x010eff;
            highscoreTextColor=0x010eff;
            break;
        }
        case 9:{
    		bgColor = 0x000000;
			orbitColor = 0xf800ff;
	        hurdleColor = 0xf800ff;
	        hurdleOrbitColor = 0xf800ff;
	        playerColor = 0xf800ff;
	        scoreTextColor=0xf800ff;
			arrow_headColor = 0xf800ff;	        
	        shareBtnColor = 0xf800ff;
	        playBtnColor = 0xf800ff;
            highscoreTextColor=0xf800ff;
            break;
        }
        case 10:{
    		bgColor = 0x000000;
			orbitColor = 0xff008f;
	        hurdleColor = 0xff008f;
	        hurdleOrbitColor = 0xff008f;
	        playerColor = 0xff008f;
	        scoreTextColor=0xff008f;
			arrow_headColor = 0xff008f;	        
	        shareBtnColor = 0xff008f;
	        playBtnColor = 0xff008f;
            highscoreTextColor=0xff008f;
            break;
        }
        case 11:{
    		bgColor = 0x000000;
			orbitColor = 0xff0050;
	        hurdleColor = 0xf93674;
	        hurdleOrbitColor = 0xf93674;
	        playerColor = 0xf93674;
	        scoreTextColor=0xf93674;
			arrow_headColor = 0xf93674;	        
	        shareBtnColor = 0xf93674;
	        playBtnColor = 0xf93674;
            highscoreTextColor=0xf93674;
            break;
        }
        case 12:{
    		bgColor = 0x000000;
			orbitColor = 0xfc3a3a;
	        hurdleColor = 0xfc3a3a;
	        hurdleOrbitColor = 0xfc3a3a;
	        playerColor = 0xfc3a3a;
	        scoreTextColor=0xfc3a3a;
			arrow_headColor = 0xfc3a3a;	        
	        shareBtnColor = 0xfc3a3a;
	        playBtnColor = 0xfc3a3a;
            highscoreTextColor=0xfc3a3a;
            break;
        }
        case 13:{
    		bgColor = 0x000000;
			orbitColor = 0xffffff;
	        hurdleColor = 0xff0000;
	        hurdleOrbitColor = 0xff0000;
	        playerColor = 0xff0000;
	        scoreTextColor=0xff0000;
			arrow_headColor = 0xff0000;	        
	        shareBtnColor = 0xff0000;
	        playBtnColor = 0xff0000;
            highscoreTextColor=0xff0000;
            break;
        }
        case 14:{
    		bgColor = 0x000000;
			orbitColor = 0xff0000;
	        hurdleColor = 0xffffff;
	        hurdleOrbitColor = 0xffffff;
	        playerColor = 0xffffff;
	        scoreTextColor=0xffffff;
			arrow_headColor = 0xffffff;	        
	        shareBtnColor = 0xffffff;
	        playBtnColor = 0xffffff;
            highscoreTextColor=0xffffff;
            break;
        }
        case 15:{
    		bgColor = 0x000000;
			orbitColor = 0xffe900;
	        hurdleColor = 0x1900ff;
	        hurdleOrbitColor = 0x1900ff;
	        playerColor = 0x1900ff;
	        scoreTextColor=0xffe900;
			arrow_headColor = 0x1900ff;	        
	        shareBtnColor = 0xffe900;
	        playBtnColor = 0xffe900;
            highscoreTextColor=0xffe900;
            break;
        }
        case 16:{
    		bgColor = 0x000000;
			orbitColor = 0x7605ff;
	        hurdleColor = 0xffbc05;
	        hurdleOrbitColor = 0xffbc05;
	        playerColor = 0xffbc05;
	        scoreTextColor=0xffbc05;
			arrow_headColor = 0xffbc05;	        
	        shareBtnColor = 0xffbc05;
	        playBtnColor = 0xffbc05;
            highscoreTextColor=0xffbc05;
            break;
        }
        case 17:{
    		bgColor = 0x000000;
			orbitColor = 0xff00ff;
	        hurdleColor = 0xfbff00;
	        hurdleOrbitColor = 0xfbff00;
	        playerColor = 0xfbff00;
	        scoreTextColor=0xfbff00;
			arrow_headColor = 0xfbff00;	        
	        shareBtnColor = 0xfbff00;
	        playBtnColor = 0xfbff00;
            highscoreTextColor=0xfbff00;
            break;
        }
        case 18:{
    		bgColor = 0x000000;
			orbitColor = 0xff0217;
	        hurdleColor = 0x17ff02;
	        hurdleOrbitColor = 0x17ff02;
	        playerColor = 0x17ff02;
	        scoreTextColor=0x17ff02;
			arrow_headColor = 0x17ff02;	        
	        shareBtnColor = 0x17ff02;
	        playBtnColor = 0x17ff02;
            highscoreTextColor=0x17ff02;
            break;
        }
        case 19:{
    		bgColor = 0x000000;
			orbitColor = 0x03fff2;
	        hurdleColor = 0xff3e03;
	        hurdleOrbitColor = 0xff3e03;
	        playerColor = 0xff3e03;
	        scoreTextColor=0xff3e03;
			arrow_headColor = 0xff3e03;	        
	        shareBtnColor = 0xff3e03;
	        playBtnColor = 0xff3e03;
            highscoreTextColor=0xff3e03;
            break;
        }
        case 20:{
    		bgColor = 0x000000;
			orbitColor = 0x020aff;
	        hurdleColor = 0xff7c02;
	        hurdleOrbitColor = 0xff7c02;
	        playerColor = 0xff7c02;
	        scoreTextColor=0xff7c02;
			arrow_headColor = 0xff7c02;	        
	        shareBtnColor = 0xff7c02;
	        playBtnColor = 0xff7c02;
            highscoreTextColor=0xff7c02;
            break;
        }
    }
}
var bgGroup;
function InitializeBackgroundOrbits() {
    bgGroup=game.add.group();
    for (var i=1;i<=3;i++){
        bgGroup.create(game.world.centerX,game.world.centerY,'bg'+i);
    }
    bgGroup.setAllChildren('anchor.x',0.5);
    bgGroup.setAllChildren('anchor.y',0.5);   

    bgGroup.children[0].scale.set(1.35);
    bgGroup.children[1].scale.set(1.45);
    bgGroup.children[2].scale.set(1.55);

    backgroundOrbitInTransition();
}
function backgroundOrbitInTransition(){
	var x;
	for (var i = 0; i < 3; i++) {
		x=game.add.tween(bgGroup.children[i].scale).from({x:bgGroup.children[i].scale.x+3,y:bgGroup.children[i].scale.y+3},500,Phaser.Easing.Linear.None,true);
	}	
	x.onComplete.add(InitiateGame);
}
function backgroundOrbitOutTransition(){
	for (var i = 0; i < 3; i++) {
		game.add.tween(bgGroup.children[i].scale).to({x:bgGroup.children[i].scale.x+3,y:bgGroup.children[i].scale.y+3},500,Phaser.Easing.Linear.None,true);		
	}	
}
function InitiateGame(){
	game.time.events.add(500,playLetsGoVoice);
	playerEnterFromCenter();
    hurdleZoomToPosition();
   	BeatBackgroundOrbit();
    for (var i = 0; i < 3; i++) {
        bgOrbitBeat[i].start();
    }
   	// BeatBG1();
   	// for (var i = 0; i < 3; i++) {
   	// 	game.add.tween(bgGroup.children[i].scale).to({x:bgGroup.children[i].scale.x+0.05,y:bgGroup.children[i].scale.y+0.05},190,Phaser.Easing.Cubic.InOut,true,0,15*2,true);
    // }
    scoreEnterFromCenter();
}
function playLetsGoVoice(){
	letsgoVoice.play('',0,1);
}
function scoreEnterFromCenter(){
	scoreText.visible=true;
	game.add.tween(scoreText.scale).from({x:0,y:0},playerZoomSpeed,"Linear",true);
    game.add.tween(scoreText.scale).to({x:1,y:1},playerZoomSpeed,"Linear",true);
}

function StartBeats(){
    BeatHurdle();
    BeatPlayerOrbits();
}


//******************************player functions
var isFired=false;
var angleCal,playerAngle;
function fire() {
    isFired=true;
    playerRevolveSpeed = 0;
    game.physics.arcade.velocityFromAngle(player.angle + 225, shootingSpeed, player.body.velocity);
}
function hitWorldBounds(){
    player_blast.play();
    GameOver();

}
function ResetPlayer(){
    player.body.velocity.x=0;
    player.body.velocity.y=0;

    player.position.x=game.world.centerX;
    player.position.y=game.world.centerY;

    game.add.tween(player.position).from({x:game.world.centerX,y:game.world.centerY},playerZoomSpeed,"Linear",true);
    game.add.tween(player.position).to({x:game.world.centerX,y:game.world.centerY},playerZoomSpeed,"Linear",true);

    game.add.tween(player.scale).from({x:0,y:0},playerZoomSpeed,"Linear",true);
    game.add.tween(player.scale).to({x:1,y:1},playerZoomSpeed,"Linear",true).onComplete.add(AllowPlayerFire);

    player.body.enable = true;
    setPlayerRandomRevolveSpeed();
    setPlayerOrbitPosition(playerOrbitPosition);
    setPlayerRandomDirection();
    
    game.add.tween(orbit.children[0].scale).to({x:1.1,y:1.1},playerZoomSpeed,"Linear",true);

}
function AllowPlayerFire(){
    isFired=false;
    playerEnterFromCenterFlag=true;
}



//******************************hurdle functions
var transitionSpeed;
function HurdleZoomToCenter(){
    beatHurdle.pause();

    var deltaX=game.world.centerX-hurdle.world.x;
    var deltaY=game.world.centerY-hurdle.world.y;

    for(var i=0;i<3;i++) {
        game.add.tween(orbit.children[i].scale).to({
            x: orbit.children[i].scale.x + 2,
            y: orbit.children[i].scale.y + 2
        }, transitionSpeed, "Linear", true).onComplete.add(ResetScale);
    }

    for(var i=0;i<3;i++) {
        game.add.tween(bgGroup.children[i].scale).to({
            x: bgGroup.children[i].scale.x + 2,
            y: bgGroup.children[i].scale.y + 2
        }, transitionSpeed, "Linear", true).onComplete.add(ResetScale);
    }
    game.add.tween(orbit.position).to({x:deltaX*5,y:deltaY*5},transitionSpeed,Phaser.Easing.Linear.Out,true).onComplete.add(ResetOrbit);
    game.add.tween(bgGroup.position).to({x:deltaX*5,y:deltaY*5},transitionSpeed,Phaser.Easing.Linear.Out,true);

    game.add.tween(player.scale).to({x:0,y:0},transitionSpeed,Phaser.Easing.Linear.Out,true);
    game.add.tween(hurdle.pivot).to({x:0,y:0},transitionSpeed,Phaser.Easing.Linear.Out,true).onComplete.add(FadeHurdle);
   
}
function ResetScale(){
    for(var i=0;i<3;i++) {
        orbit.children[i].scale.set(1.1);
    }
    bgGroup.children[0].scale.set(1.35);
    bgGroup.children[1].scale.set(1.45);
    bgGroup.children[2].scale.set(1.55);
}
function ResetOrbit() {
    orbit.scale.set(0);
    orbit.x=0;
    orbit.y=0;
    game.add.tween(orbit.position).from({x:game.world.centerX,y:game.world.centerY},transitionSpeed,Phaser.Easing.Linear.Out,true);
    game.add.tween(orbit.scale).to({x:1,y:1},transitionSpeed,Phaser.Easing.Linear.Out,true);

    bgGroup.scale.set(0);
    bgGroup.x=0;
    bgGroup.y=0;
    game.add.tween(bgGroup.position).from({x:game.world.centerX,y:game.world.centerY},transitionSpeed,Phaser.Easing.Linear.Out,true);
    game.add.tween(bgGroup.scale).to({x:1,y:1},transitionSpeed,Phaser.Easing.Linear.Out,true);
}
function FadeHurdle(){
    game.add.tween(hurdle).to({alpha:0},transitionSpeed,Phaser.Easing.Linear.Out,true).onComplete.add(newHurdleZoom);
    game.add.tween(hurdleOrbit).to({alpha:0},transitionSpeed,Phaser.Easing.Linear.Out,true);

}
var beatSpeed,beatHurdle,beatFrequency;
function BeatHurdle(){
    beatHurdle=game.add.tween(hurdleOrbit.scale).to({x:beatFrequency,y:beatFrequency},beatSpeed,Phaser.Easing.Linear.None,true,0,-1,true);
}
var hurdleOn1Quadrant=false,hurdleOn4Quadrant=false;
var hurdleOn2Quadrant=false,hurdleOn3Quadrant=false;
function MoveHurdle(){
    if(hurdleOn1Quadrant || hurdleOn4Quadrant){
        hurdle.position.x-=0.5;
        hurdleOrbit.position.x-=0.5;
    }
    else if(hurdleOn2Quadrant || hurdleOn3Quadrant){
        hurdle.position.x+=0.5;
        hurdleOrbit.position.x+=0.5;
    }
}

//******************************update functions
function RotatePlayerOnOrbit() {
    if(rotatePlayer) {
        player.body.rotation += playerRevolveSpeed * 100;
    }
}
function RotateHurdle(){
    if(rotatePlayer) {
        hurdle.body.rotation += hurdleRevolveSpeed;
    }
}

function ReduceOrbitWithTime(){
    //6 seconds of tween is the exact point where player and orbit moves correctly to the center
    //then if 5 seconds then 0.07 for pivot, if 7 sec then 0.05 pivot decrement and so on.
    if(!isFired) {
        player.pivot.x -= 0.06;
        player.pivot.y -= 0.06;
        orbit.children[0].scale.x -= 0.0006;
        orbit.children[0].scale.y -= 0.0006;
    }
}
var giveWarning=true;
function DestroyPlayerWithTime() {
    if(player.pivot.x<100){
        player_blast.play();
		GameOver();
    }
}
function hitHurdle() {
    CheckLevel();
    levelCount++;
    BeatScore();
    perfectHitCheck();
    LevelTransition();
}
function CheckLevel() {
    if(levelCount%3==0){
        colorsChange();
    }
    if(levelCount<=3){
    	arrow_head.alpha-=0.25;
    }
}
function RotateOrbits() {
    orbit.children[2].rotation+=orbitRevolveSpeed;
    orbit.children[1].rotation-=orbitRevolveSpeed;
}
var warningTween;
function InitializeWarning(){
    warningTween=game.add.tween(orbit.children[1]).to({alpha: 1}, 200, Phaser.Easing.Linear.None,true,0,-1,true);
}
function GiveWarning(){
    if(!isFired && giveWarning) {
        warningTween.pause();
        game.add.tween(orbit.children[1]).to({alpha: 0.25}, 100, Phaser.Easing.Linear.None,true);
        if (player.pivot.x < 110) {
            giveWarning = false;
            warningTween.resume();
        }
    }
}

var colorsChanged=1;
var flashFlag=false;
var colorsArray=[];
function setColorsArray(){
	var k=1;
	for (var i = 0; i < 20; i++) {
		colorsArray[i]=k;
		k++;
	}
	shuffle(colorsArray);
}
function shuffle(array) {
    for (var i = array.length - 1; i > 0; i--) {
        var j = Math.floor(Math.random() * (i + 1));
        var temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    return array;
}

function colorsChange(){
    InitializeGameColors(colorsArray[0]);
    setGameColors();
    colorsChanged++;
    flashFlag=true;
    cameraFlashOnce();
    shuffle(colorsArray);
}
//******************************setters
function setPlayerOrbitPosition(playerOrbitPosition) {
    player.pivot.x = playerOrbitPosition;
    player.pivot.y = playerOrbitPosition;
}
var orbitRevolveSpeed;
function setPlayerRandomRevolveSpeed(){
    playerRevolveSpeed=game.rnd.realInRange(minRevolveSpeed,maxRevolveSpeed);
    orbitRevolveSpeed=playerRevolveSpeed;
}

function setPlayerRandomDirection(){
    var x=game.rnd.realInRange(-1,1);
    if(x>0){
        playerRevolveSpeed*=1;
    }
    else{
        playerRevolveSpeed*=-1;
    }
}
var hurdleRevolveSpeed;
var hurdlePosition;
function setHurdleOrbitPosition(){
    var r=game.rnd.integerInRange(0,2);
    switch(r){
        case 0:
            hurdle.pivot.x=250;
            hurdle.pivot.y=150;
            hurdlePosition=0;
            break;
        case 1:
            hurdle.pivot.x=350;
            hurdle.pivot.y=150;
            hurdlePosition=1;
            break;
        case 2:
            hurdle.pivot.x=450;
            hurdle.pivot.y=150;
            hurdlePosition=2;
            break;
    }

    if(hurdlePosition==0){        	
    	hurdle.angle=game.rnd.integerInRange(-180,180);
    	var x=game.rnd.realInRange(-1,1);
	    if(x>0){
	        hurdleRevolveSpeed=0.25;
	    }
	    else{
	        hurdleRevolveSpeed=-0.25;
	    }
    }
    else if(hurdlePosition==1){
    	var x=game.rnd.integerInRange(1,4);
    	switch(x){
    		case 1:
    			//first quad
    			hurdle.angle=game.rnd.integerInRange(90,130);
    			hurdleRevolveSpeed=-0.1;
    			break;
    		case 2:
    			//2nd quad
    			hurdle.angle=game.rnd.integerInRange(10,90);
		        hurdleRevolveSpeed=0.1;			
    			break;
    		case 3:
    			//3rd quad
    			hurdle.angle=game.rnd.integerInRange(-45,-90);
    			hurdleRevolveSpeed=-0.1;    			
    			break;
    		case 4:
    			//4th quad
    			hurdle.angle=game.rnd.integerInRange(-150,-90);
		        hurdleRevolveSpeed=0.1;
    			break;
    	}
    }
    else if(hurdlePosition==2){
    	hurdleRevolveSpeed=0;
    	var x=game.rnd.integerInRange(1,4);
    	switch(x){
    		case 1:
    			hurdle.angle=35;
    			break;
    		case 2:
    			hurdle.angle=-75;
    			break;
    		case 3:
    			hurdle.angle=-145;
    			break;
    		case 4:
    			hurdle.angle=105;
    			break;
    	}
    }

    //fade other orbits
    for(var i=0;i<3;i++){
    	if(i!=hurdlePosition){
    		bgGroup.children[i].alpha=0.4;
    	}
    }

}
function setGameColors(){
    game.stage.backgroundColor=bgColor;
    player.tint=playerColor;
    arrow_head.tint=arrow_headColor;
    hurdle.tint=hurdleColor;
    hurdleOrbit.tint=hurdleOrbitColor;
    playBtn.tint=playBtnColor;
    shareBtn.tint=shareBtnColor;
    scoreText.tint=scoreTextColor;
    highscoreText.tint=highscoreTextColor;

    for(var i=0;i<3;i++){
        if(i!=1)
            orbit.children[i].tint=orbitColor;
    }
    for(var i=0;i<3;i++){
        bgGroup.children[i].tint=orbitColor;
    }
    orbit.children[1].tint=0xff0000;
    orbit.children[1].alpha=0.25;
}


//******************************Extra Functions
function SetCircleCollider(sprite) {
    var radius = sprite.width / 2;

    sprite.body.setCircle(
        radius,
        (-radius + 0.5 * sprite.width  / sprite.scale.x),
        (-radius + 0.5 * sprite.height / sprite.scale.y)
    );
}
function cameraShake() {
    game.camera.shake(0.05,400);
}
function AddScore() {
    score+=1;    
    if(comboCount>2){
    	perfectHitText.tint=0xff0000;
    	BeatPerfectHitText();       
    	perfectHitText.text=comboTextArr[comboTextArr.length-1];    	
	}
	scoreText.text=score;
    comboCount=0;
}
//the last text in the following array is used when the player losses a perfect hit streak
var comboTextArr=["PERFECT HIT!", "BULL'S EYE!", "SMASHING!","AWESOME!","DEAD CENTRE","CLOSE!"];
var comboTextCount=0;
function perfectHitCheck() {
    var angleCal = calculateAngle();
    var x = angleCal - 5;
    var y = angleCal + 5;
    var playerAngle = player.angle + 225;

    if (playerAngle >= x && playerAngle <= y) {
        perfectHitFlash();
        perfecthit.play();
        AddPerfectHitScore();
        BeatPerfectHitText();       
        perfectHitText.text=comboTextArr[comboTextCount];
        // playVoiceOver(comboTextCount);
        comboTextCount++;
        if(comboTextCount==comboTextArr.length-1)
        	comboTextCount=0;
    }
    else {
        AddScore();
        comboTextCount=0;        
        hurdle_blast.play();
    }
}
function playVoiceOver(num){
	switch(num){
		case 0:
			game.time.events.add(500,playPerfecthitvoice);
			break;
		case 1:
			game.time.events.add(500,playBullseyeVoice);
			break;
		case 2:
			game.time.events.add(500,playSmashingVoice);
			break;
		case 3:
			game.time.events.add(500,playAwesomeVoice);
			break;
		case 4:
			game.time.events.add(500,playDeadcenterVoice);
			break;				
	}
}
function playPerfecthitvoice(){
	perfecthitVoice.play('',0,1);
}

function playBullseyeVoice(){
	bullseyeVoice.play('',0,1);		
}

function playSmashingVoice(){
	smashingVoice.play('',0,1);		
}
function playAwesomeVoice(){
	awesomeVoice.play('',0,1);		
}

function playDeadcenterVoice(){
	deadcenterVoice.play('',0,1);		
}

var comboCount=0;//this is reset to 0 when the player hits a non perfect shot
function AddPerfectHitScore() {
    score+=3+comboCount;
    comboCount++;
    scoreText.text=score;
}
function perfectHitFlash(){
    game.camera.flash(0xffd700, 400);
}
function cameraFlash(){
    game.camera.flash(orbitColor, 400);
}
function cameraFlashOnce() {
    if(flashFlag){
        flashFlag=false;
        cameraFlash();
    }
}
function calculateAngle(){
    var x1,x2,y1,y2;
    x1=player.x;
    x2=hurdle.world.x;
    y1=player.y;
    y2=hurdle.world.y;
    // angle in degrees
    var angleDeg = Math.atan2(y2 - y1, x2 - x1) * 180 / Math.PI;
    if(angleDeg<0)
        angleDeg+=360;
    return angleDeg;
}
function LevelTransition() {
    player.body.enable = false;
    HurdleZoomToCenter();
}
function newHurdleZoom(){
	//fade other orbits
    for(var i=0;i<3;i++){
    	bgGroup.children[i].alpha=1;
    }

    hurdle.visible=true;
    hurdleOrbit.visible=true;

    hurdle.position.x=game.world.centerX;
    hurdle.position.y=game.world.centerY;

    hurdleOn1Quadrant=hurdleOn2Quadrant=hurdleOn3Quadrant=hurdleOn4Quadrant=false;
    setHurdleOrbitPosition();

    game.add.tween(hurdle).to({alpha:1},transitionSpeed,Phaser.Easing.Linear.Out,true);
    game.add.tween(hurdle.scale).from({x:0,y:0},transitionSpeed,Phaser.Easing.Linear.Out,true);
    game.add.tween(hurdle.scale).to({x:1,y:1},transitionSpeed,Phaser.Easing.Linear.Out,true);
    game.add.tween(hurdleOrbit).to({alpha:1},transitionSpeed,Phaser.Easing.Linear.Out,true);
    game.add.tween(hurdleOrbit.scale).from({x:0,y:0},transitionSpeed,Phaser.Easing.Linear.Out,true);
    game.add.tween(hurdleOrbit.scale).to({x:1,y:1},transitionSpeed,Phaser.Easing.Linear.Out,true);

    giveWarning=true;
    warningTween.pause();
    beatHurdle.resume();
    player.visible=true;

    hurdle.body.enable=true;

    ResetPlayer();

}
var scorebeatSpeed;
function BeatScore(){
    game.add.tween(scoreText.scale).to({x: beatFrequency, y: beatFrequency}, scorebeatSpeed, Phaser.Easing.Linear.Out, true,0,1,true);
    game.add.tween(scoreText).to({alpha:1}, scorebeatSpeed, Phaser.Easing.Linear.Out, true,0,1,true);
}
function BeatPerfectHitText(){
    game.add.tween(perfectHitText).to({alpha:1}, scorebeatSpeed, Phaser.Easing.Linear.Out, true,0,1,true).onComplete.add(function resetTint(){perfectHitText.tint=0xffcc00;});
}
var clicked = false;
var gameoverFlag=false;
function RestartGame(){
	game.add.tween(scoreText.position).to({x:game.world.centerX,y:game.world.centerY},playerZoomSpeed,"Linear",true);
	game.add.tween(scoreText.scale).to({x:1,y:1},playerZoomSpeed,"Linear",true);
	game.add.tween(scoreText).to({alpha:0.25},playerZoomSpeed,"Linear",true);

	game.add.tween(highscoreText.position).to({x:game.world.centerX,y:game.world.centerY},playerZoomSpeed,"Linear",true);
	game.add.tween(highscoreText.scale).to({x:1,y:1},playerZoomSpeed,"Linear",true);
	game.add.tween(highscoreText).to({alpha:0},playerZoomSpeed,"Linear",true);

    shareBtn.inputEnabled=false;
    game.add.tween(shareBtn.position).to({x:game.world.centerX,y:game.world.centerY},playerZoomSpeed,"Linear",true);
    game.add.tween(shareBtn.scale).to({x:1,y:1},playerZoomSpeed,"Linear",true);
    game.add.tween(shareBtn).to({alpha:0},playerZoomSpeed,"Linear",true);    

	HideRestartButton();
	// beat.fadeIn(1000,true);
	game.add.tween(beat).to({volume:1},1000,"Linear",true);
	// game.time.events.add(500,playLetsGoVoice);
	shuffle(colorsArray);
	arrow_head.alpha=1;
	clicked = true;
    levelCount=0;
	game.input.activePointer.reset();
	gameoverFlag=false;
    colorsChanged=1;
    comboCount=0;
	comboTextCount=0;
    flashFlag=false;
    perfectHitText.alpha=0;    
    isFired=false;
    score=0;
    scoreText.text=score.toString();
    giveWarning=true;
    hurdleOn1Quadrant=hurdleOn2Quadrant=hurdleOn3Quadrant=hurdleOn4Quadrant=false;
    orbit.setAllChildren('scale.x',1.1);
    orbit.setAllChildren('scale.y',1.1);

    //BACKGROUND ORBITS IN TRANSITION
    var x;
    for (var i = 0; i < 3; i++) {
        x=game.add.tween(bgGroup.children[i].scale).to({x:bgGroup.children[i].scale.x-3,y:bgGroup.children[i].scale.y-3},500,Phaser.Easing.Linear.None,true);
    }   
    x.onComplete.add(resumeBgOrbitBeat);   
    newHurdleZoom();
}

var bgOrbitBeat=[];
function BeatBackgroundOrbit() {
    for (var i=0;i<3;i++){
        bgOrbitBeat[i] = game.add.tween(bgGroup.children[i].scale).to({x:bgGroup.children[i].scale.x+0.05,y:bgGroup.children[i].scale.y+0.05},beatSpeed,Phaser.Easing.Linear.Out,false,0,-1,true);
    }
}
var beatPlayerOrbits=[];
function BeatPlayerOrbits(){
    beatPlayerOrbits[1]=game.add.tween(orbit.children[1].scale).to({x:orbit.children[1].scale.x-0.1,y:orbit.children[1].scale.y-0.1},beatSpeed,Phaser.Easing.Linear.Out,true,0,-1,true);
    beatPlayerOrbits[2]=game.add.tween(orbit.children[2].scale).to({x:orbit.children[2].scale.x-0.05,y:orbit.children[2].scale.y-0.05},beatSpeed,Phaser.Easing.Linear.Out,true,0,-1,true);
}
function GameOver() {

	// gameoverVoice.play('',0,0.5);
	// beat.fadeOut(1000);
	game.add.tween(beat).to({volume:0.25},1000,"Linear",true);
	gameoverFlag=true;
	cameraFlash();
	cameraShake();
    player.body.velocity.x=0;
    player.body.velocity.y=0;
    player.visible=false;
    clicked = false;
    hurdle.visible=false;
    hurdleOrbit.visible=false;
	
	// spaceBar.onDown.addOnce(RestartGame);
    
    //BACKGROUND ORBITS OUT TRANSITION
    var x;
    for (var i = 0; i < 3; i++) {
        x=game.add.tween(bgGroup.children[i].scale).to({x:bgGroup.children[i].scale.x+3,y:bgGroup.children[i].scale.y+3},500,Phaser.Easing.Linear.None,true);
    }   
    x.onComplete.add(pauseBgOrbitBeat);    
    
    CheckHighScore();
	SetHighScore();
	GameOverScore();
}
function CheckHighScore(){
	if(score>highscore){
		localStorage.setItem("orbitBeatHighScore",score);
	}
}
function SetHighScore() {
	highscore = parseInt(localStorage.getItem("orbitBeatHighScore"));
}
function pauseBgOrbitBeat(){
    for (var i = 0; i < 3; i++) {
        bgOrbitBeat[i].pause();
    }
}
function resumeBgOrbitBeat(){
    for (var i = 0; i < 3; i++) {
        bgOrbitBeat[i].resume();
    }
}
function GameOverScore() {
	scoreText.text="SCORE:"+score;
    game.add.tween(scoreText.scale).to({x:1.5,y:1.5},transitionSpeed,Phaser.Easing.Linear.InOut,true);
    game.add.tween(scoreText).to({alpha:1},transitionSpeed,Phaser.Easing.Linear.InOut,true);
    game.add.tween(scoreText.position).to({x:game.world.centerX,y:game.world.centerY-300},transitionSpeed,Phaser.Easing.Linear.Out,true).onComplete.add(ShowRestartButton);

	highscoreText.text="BEST:"+highscore;
	game.add.tween(highscoreText.scale).to({x:1.5,y:1.5},transitionSpeed,Phaser.Easing.Linear.InOut,true);
    game.add.tween(highscoreText).to({alpha:1},transitionSpeed,Phaser.Easing.Linear.InOut,true);
    game.add.tween(highscoreText.position).to({x:game.world.centerX,y:game.world.centerY+300},transitionSpeed,Phaser.Easing.Linear.Out,true);

    game.add.tween(shareBtn.scale).to({x:0.5,y:0.5},transitionSpeed,Phaser.Easing.Linear.InOut,true);
    game.add.tween(shareBtn).to({alpha:1},transitionSpeed,Phaser.Easing.Linear.InOut,true);
    var x=game.add.tween(shareBtn.position).to({x:game.world.centerX,y:game.world.centerY+400},transitionSpeed,Phaser.Easing.Linear.Out,true);
    x.onComplete.add(function startShareBtn(){shareBtn.inputEnabled=true;});
}
function ShowRestartButton() {
	game.add.tween(playBtn).to({alpha:1},transitionSpeed,Phaser.Easing.Linear.InOut,true);
	playBtn.inputEnabled=true;
	playBtn.visible=true;
    playBtn.events.onInputDown.add(RestartGame, this);
    
}
function HideRestartButton() {
	game.add.tween(playBtn).to({alpha:0},playerZoomSpeed,Phaser.Easing.Linear.InOut,true);
	playBtn.inputEnabled=false;
	playBtn.visible=false;
}

var bootState={    
    init: function(){        
        game.scale.pageAlignHorizontally = true;        
        game.scale.pageAlignVertically = true;        
        game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;        
        game.scale.fullScreenScaleMode = Phaser.ScaleManager.SHOW_ALL;    
    },    
    preload:function () {        
        game.load.image('splash', 'assets/splashScreen1.png');        
        game.load.image('progressbar', 'assets/progressline.png'); 
        game.load.image('progressbarborder', 'assets/progresslineborder.png'); 
    },    
    create:function () {        
        game.state.start("loadState");    
    }
}

var loadState={
	preload:function () {
		splash=game.add.image(0, -100, 'splash');     
		splash.scale.set(1,1.25);   
		
		loadingText=game.add.text(game.world.centerX-50,game.world.centerY+300, "LOADING",  { font: '30px myFirstFont', fill: '#fff' ,align:"center"});        
		loadingText.anchor.set(0.5,0.5);        
		loadingTextScore=game.add.text(game.world.centerX+50,game.world.centerY+300, '',  { font: '30px myFirstFont', fill: '#fff' ,align:"center"});        
		loadingTextScore.anchor.set(0.5,0.5);        
    
 		preLoadBarBorder=game.add.sprite(game.world.width/2 -240, 3*(game.world.height)/4, 'progressbarborder');
        preLoadBarBorder.anchor.set(0,0.5);

        preLoadBar=game.add.sprite(game.world.width/2 -240, 3*(game.world.height)/4, 'progressbar');
        preLoadBar.anchor.set(0,0.5);

		this.load.setPreloadSprite(preLoadBar,0);

		game.load.audio('orbeatVoice','assets/voice/orbeat.wav');
		game.load.audio('awesomeVoice','assets/voice/awesome.wav');
		game.load.audio('bullseyeVoice','assets/voice/bullseye.wav');
		game.load.audio('deadcenterVoice','assets/voice/deadcenter.wav');
		game.load.audio('gameoverVoice','assets/voice/gameover.wav');
		game.load.audio('letsgoVoice','assets/voice/letsgo.wav');
		game.load.audio('perfecthitVoice','assets/voice/perfecthit.wav');
		game.load.audio('smashingVoice','assets/voice/smashing.wav');

        game.load.image('emitter','assets/emitter.png');
        game.load.image('playBtn','assets/playBtn.png');
        game.load.image('orbit1','assetsNeon/orbit1.png');
        game.load.image('orbit2','assetsNeon/orbit2.png');
        game.load.image('orbit3','assetsNeon/orbit3.png');
        game.load.audio('introBeat','assets/introBeat.mp3');
        // game.load.audio('introBeat','assets/introBeat.ogg');//This won't run in Safari
        game.load.audio('titleImpact','assets/titleImpact.wav');
        game.load.audio('titleImpact2','assets/titleImpact2.wav');

		game.load.json('myjson','assets/myjson.json');

        game.load.image('playBtn','assets/playBtn.png');
        game.load.image('shareBtn','assets/shareBtn.jpg');
        game.load.image('fullscreenBtn','assets/fullscreenBtn.png');

        game.load.image('player','assets/player.png');
        game.load.image('arrow_head','assets/arrow_head.png');
        game.load.image('orbit1','assetsNeon/orbit1.png');
        game.load.image('orbit2','assetsNeon/orbit2.png');
        game.load.image('orbit3','assetsNeon/orbit3.png');

        game.load.image('hurdle','assets/hurdle.png');
        game.load.image('hurdleOrbit','assets/hurdleOrbit.png');

        game.load.image('bg1','assetsNeon/bg1.png');
        game.load.image('bg2','assetsNeon/bg2.png');
        game.load.image('bg3','assetsNeon/bg3.png');

        game.load.audio('hurdle_blast','assets/hurdle_blast.wav');
        game.load.audio('player_blast','assets/player_blast.wav');
        game.load.audio('perfecthit','assets/perfecthit.wav');
        game.load.audio('fireLaunch','assets/fireLaunch.wav');
        game.load.audio('beat','assets/beatHalf.mp3');
        // game.load.audio('beat','assets/beatHalf.ogg');
    },
    create:function(){
    	game.state.start("playState");
    },
     loadUpdate:function() {        
     	// update loading text percent       
    	loadingTextScore.text=this.load.progress + "%";    
 	}
}
var testState={
	preload:function(){
	    game.load.image('bg1','assetsNeon/bg1.png');
        game.load.image('bg2','assetsNeon/bg2.png');
        game.load.image('bg3','assetsNeon/bg3.png');	
        game.load.audio('beat','assets/beatHalf.ogg');
	},
	create:function(){
		SetEnvironment();
		beat = game.add.audio('beat');
		BGMusic();		        
		bgGroup=game.add.group();
	    for (var i=1;i<=3;i++){
	        bgGroup.create(game.world.centerX,game.world.centerY,'bg'+i);
	    }
	    bgGroup.setAllChildren('anchor.x',0.5);
	    bgGroup.setAllChildren('anchor.y',0.5);

	    bgGroup.children[0].scale.set(1.35);
	    bgGroup.children[1].scale.set(1.45);
	    bgGroup.children[2].scale.set(1.55);
	    BeatBG1();
	},
	render:function(){
		game.debug.text(count,100,100);
	}
}

var count=0;
function BeatBG1(){
	var x;
	if(count<3){
		for (var i=0;i<3;i++){
	       	x=game.add.tween(bgGroup.children[i].scale).to({x:bgGroup.children[i].scale.x+0.05,y:bgGroup.children[i].scale.y+0.05},190,Phaser.Easing.Linear.None,true,0,15*2,true);
	    }
	    x.onComplete.add(BeatBG2);
	}
	else{
		for (var i=0;i<3;i++){
	       	x=game.add.tween(bgGroup.children[i].scale).to({x:bgGroup.children[i].scale.x+0.1,y:bgGroup.children[i].scale.y+0.1},750,Phaser.Easing.Cubic.In,true,0,7,true);
	    }
	    x.onComplete.add(BeatBG2);
	}
}
function BeatBG2(){
	var x;
	for (var i=0;i<3;i++){
       	x=game.add.tween(bgGroup.children[i].scale).to({x:bgGroup.children[i].scale.x+0.1,y:bgGroup.children[i].scale.y+0.1},100,Phaser.Easing.Cubic.InOut,true,0,1,true);
    }
    if(count==3){
    	count=0;
    }else{
    	count++;	
    }
    x.onComplete.add(BeatBG1);
    
}

game.state.add("bootState",bootState);
game.state.add("loadState",loadState);
game.state.add("playState",playState);
game.state.add("mainState",mainState);
game.state.add("testState",testState);
game.state.start("bootState");

function shareScore(){
     FB.ui({
        method: 'share',
        display: 'popup',
        href: 'http://ms-mf.net/html_games/Orbeat/index.html',
        quote: "I scored " + highscore + " points on Orbeat. BEAT THAT!",
        hashtag:"#Orbeat!",
     }, function(response){});
}