from matplotlib import pyplot as plot
from basketball_reference_scraper.players import get_stats

x = get_stats('Lebron James', stat_type = "PER_GAME")
print(x['PTS'])

y = [1,2,3,4,5,6,7,8,0,10,11,12,13,14,15,16,17,18,19,20,21]
x1 = list(map(float, x['PTS']))
y1 = list(map(float, x['AST']))
sort = false
count = 0
plot.scatter(x1,y1)

plot.show()
