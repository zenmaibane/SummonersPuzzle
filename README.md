# SummonersPuzzle
ハッカソン系インターンで作ったリアルタイムアクションパズルゲーム「サマナーズパズル」

公開していいとのことだったので公開しました．また，それにあたって使ったリソース(フォント等)の一部をリポジトリから消しています．

エンジニア2名，デザイナー1名で作成．実装期間は1週間ほど．

自分はアウトゲーム全般，インゲームの一部機能・演出を担当しました．

# ゲームルール

1. 画面下部に出てくるブロックを画面上部にシュートして同じ色+同じランク隣接するブロックと合体させ，ランクを上げる​．隣のブロックと合体させる事が可能で，最終的にそれらの合算値がランクになる．
2. 一定時間ごとに画面最上部のブロックがランクに関係なく召喚される．​ランクが高い程ダメージが高いが，もちろん低いままだと全くダメージを与えられない．
3. 合体させる際にブーストゲージが溜まっていき，​満タンになった際に召喚の際のダメージに倍率がかかる．連鎖させるとゲージがたまりやすくなる．

![howto](SummonersPuzzleProject/Assets/Images/HowTo/bg.png)

キャラごとにタイプが別れていて，色が多いけどブロックの初期ランクが高いパワー型・色は単一のかわりにブロックの初期ランクが低いスピード型・バランス型が存在する．また．キャラごとにブロックが消える時間が異なる．


# デモプレイ
デモプレイは一人プレイのものです．

[動画はこちらから](SummonersPuzzleProject/Demo/DemoPlay.mp4)
