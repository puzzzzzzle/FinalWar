import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by zhangjiakai on 2017/7/13.
 */
public class Check {
    public static List<String> check(String sql){
        Connection con = null;
        List<String> players = new ArrayList<>();
        try {
            con = Source.getConnection();
            Statement st = con.createStatement();
            ResultSet rt = st.executeQuery(sql);
            while(rt.next()){
                players.add(rt.getString(1));
            }
            con.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return players;
    }

}
