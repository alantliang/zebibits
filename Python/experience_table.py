'''
Generate experience table for pet leveling
'''

import math
import csv


def main():
    with open('experience_table.csv', 'wb') as csvfile:
        points = 0
        csv_writer = csv.writer(csvfile)
        for level in range(1, 100):
            diff = int(level + 300 * math.pow(2, float(level) / 7))
            points += diff
            cur_level = level + 1
            cur_points = points / 4
            csv_writer.writerow([cur_level, cur_points])

if __name__ == "__main__":
    main()
