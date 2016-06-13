import java.awt.image.BufferedImage;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.stream.Collectors;

import javax.imageio.ImageIO;

/**
 * TMX-CSV and CSV-TMX map converter.
 * CSV map representation is customized for Everfire game.
 * @author Snow Arts
 * @license <a href="https://www.binpress.com/license/view/l/454c86cbd29fdd35b99910eb5b8fed5b">
 * 	TiledLayerConverter License</a>
 */
public class TiledLayerConverter {

	// CONFIGURATION OPTIONS
	
	// Swap input/output extensions for reverse conversion
	private static final String INPUT_FILE = "StandardMap.tmx"; // ensure data encoding is CSV
	private static final String OUTPUT_FILE = "StandardMap.csv";

	// Conversion constants
	private static final int WIDTH = 50, HEIGHT = 50; // fixed for all layers
	
	private static final char LAYER_SEPARATOR = '~';

	/** Map TMX values to CSV values.
	 * This list contains mappers for every layer. */
	private static final ArrayList<Map<Integer, Integer>> VALUE_MAPPERS = initValueMappers();

	private static final ArrayList<Map<Integer, Integer>> initValueMappers() {
		ArrayList<Map<Integer, Integer>> layerMappers = new ArrayList<>();
		
		// [!] Ensure values are different for a same layer [!]
		
		// Ground Layer (0)
		HashMap<Integer, Integer> groundMapper = new HashMap<>();
		
		groundMapper.put(1, 0); // Grass
		groundMapper.put(49, 1); // Sand
		groundMapper.put(160, 2); // Water
		
		layerMappers.add(groundMapper);
		
		// Object Layer (1)
		HashMap<Integer, Integer> objectMapper = new HashMap<>();
		
		objectMapper.put(175, 1); // Tree
		objectMapper.put(159, 2); // Stone
		
		layerMappers.add(objectMapper);
		
		return layerMappers;
	}
	
	// CSV to TMX conversion constants
	private static final String TILESET = "Test.png";
	
	private static final int TILE_WIDTH = 48, TILE_HEIGHT = 48;
	
	private static final ArrayList<String> LAYER_NAMES = new ArrayList<>(Arrays.asList("Ground", "Object"));

	// DON'T TOUCH ANYTHING AFTER THIS LINE
	public static void main(String[] args) {
		if (INPUT_FILE.toLowerCase().endsWith("tmx"))
			TMXtoCSV();
		else
			CSVtoTMX();
	}

	private static void TMXtoCSV() {
		try (FileWriter fw = new FileWriter(new File(OUTPUT_FILE));
				BufferedWriter bw = new BufferedWriter(fw)) {

			ArrayList<Integer[][]> maps = getMaps(getLayersFromTMX(fileToString(new File(INPUT_FILE))));
			int totalMaps = maps.size();

			for (int i = 0; i < HEIGHT; ++i) {
				for (int j = 0; j < WIDTH; ++j) {
					StringBuilder mixedCell = new StringBuilder();
					for (int m = 0; m < totalMaps; ++m) {
						Integer value = maps.get(m)[i][j];
						Integer mappedValue = VALUE_MAPPERS.get(m).get(value);
						mixedCell.append(mappedValue != null ? mappedValue : value);
						if (m < totalMaps - 1)
							mixedCell.append(LAYER_SEPARATOR);
					}
					if (i < HEIGHT - 1 || j < WIDTH - 1) // if not is the last cell
						mixedCell.append(";");
					bw.write(mixedCell.toString());
				}
				bw.write("\n");
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private static void CSVtoTMX() {
		try (FileWriter fw = new FileWriter(new File(OUTPUT_FILE));
				BufferedWriter bw = new BufferedWriter(fw)) {
			BufferedImage tileset = ImageIO.read(new File(TILESET));
			final int tilesetHeight = tileset.getHeight();
			final int tilesetWidth = tileset.getWidth();
			final int columns = tilesetWidth/TILE_WIDTH;
			final int tileCount = (tilesetHeight/TILE_HEIGHT)*columns;
			
			bw.append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n"
					+ "<map version=\"1.0\" orientation=\"orthogonal\" renderorder=\"right-down\" "
					+ "width=\"" + WIDTH + "\" height=\"" + HEIGHT + "\" tilewidth=\""
					+ TILE_WIDTH + "\" tileheight=\"" + TILE_HEIGHT + "\" nextobjectid=\"1\">\n"
					+ "<tileset firstgid=\"1\" name=\"" + TILESET.substring(0, TILESET.lastIndexOf('.'))
					+ "\" tilewidth=\"" + TILE_WIDTH + "\" tileheight=\"" + TILE_HEIGHT + "\" tilecount=\""
					+ tileCount + "\" columns=\"" + columns + "\">\n<image source=\""
					+ TILESET + "\" width=\"" + tilesetWidth
					+ "\" height=\"" + tilesetHeight + "\"/>\n</tileset>\n");
			
			String[] layers = getLayersFromCSV(fileToString(new File(INPUT_FILE)));
			for (int i = 0; i < layers.length; ++i) {
				bw.append("<layer name=\"" + LAYER_NAMES.get(i) + "\" width=\"" + WIDTH
						+ "\" height=\"" + HEIGHT + "\">\n<data encoding=\"csv\">\n");
				bw.append(layers[i]);
				bw.append("</data>\n</layer>\n");
			}
			bw.append("</map>\n");
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private static String[] getLayersFromTMX(String tmx) {
		ArrayList<String> layers = new ArrayList<>();
		Matcher m = Pattern.compile("<data.*>([\\s\\S]+?)<\\/data>").matcher(tmx);

		while (m.find())
			layers.add(m.group(1).trim());

		return layers.toArray(new String[layers.size()]);
	}
	
	private static String[] getLayersFromCSV(String csv) {
		final String layerSeparator = Character.toString(LAYER_SEPARATOR);
		final int totalLayers = csv.substring(0, csv.indexOf(';')).split(layerSeparator).length;
		String[] layers = new String[totalLayers];
		
		String[] rows = csv.split("\n");
		for (int l = 0; l < totalLayers; ++l) {
			StringBuilder layer = new StringBuilder();
			for (int i = 0; i < HEIGHT; ++i) {
				String[] cols = rows[i].split(";");

				for (int j = 0; j < WIDTH; ++j) {
					String value = cols[j].split(layerSeparator)[l];
					String mappedValue = REVERSE_MAPPERS.get(l).get(value);
					layer.append(mappedValue != null ? mappedValue : value);
					if (i < HEIGHT - 1 || j < WIDTH - 1) // if not is the last cell
						layer.append(",");
				}
				layer.append("\n");
			}
			layers[l] = layer.toString();
		}
		
		return layers;
	}

	private static ArrayList<Integer[][]> getMaps(String[] layers) {
		int size = layers.length;
		ArrayList<Integer[][]> maps = new ArrayList<>(size);

		for (String layer : layers) {
			String[] rows = layer.split("\n");
			Integer[][] map = new Integer[HEIGHT][WIDTH];

			for (int i = 0; i < HEIGHT; ++i) {
				String[] cols = rows[i].split(",");

				for (int j = 0; j < WIDTH; ++j)
					map[i][j] = Integer.parseInt(cols[j]);
			}
			maps.add(map);
		}

		return maps;
	}

	private static String fileToString(File f) throws IOException {
		FileReader fr = new FileReader(f);
		BufferedReader br = new BufferedReader(fr);
		StringBuilder sb = new StringBuilder();

		String line;
		while ((line = br.readLine()) != null)
			sb.append(line).append("\n");

		br.close();
		fr.close();
		return sb.toString();
	}
	
	/** Map CSV values to TMX values.
	 * This list contains mappers for every layer. */
	private static final ArrayList<Map<String, String>> REVERSE_MAPPERS = initReverseMappers();

	private static ArrayList<Map<String, String>> initReverseMappers() {
		ArrayList<Map<String, String>> reverseMappers = new ArrayList<>(VALUE_MAPPERS.size());
		for (Map<Integer, Integer> valueMapper : VALUE_MAPPERS)
			reverseMappers.add(valueMapper.entrySet().stream()
					.collect(Collectors.toMap(e -> e.getValue().toString(), e -> e.getKey().toString())));
		return reverseMappers;
	}
	
}
