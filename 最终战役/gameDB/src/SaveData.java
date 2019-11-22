import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.Writer;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.Statement;
import java.util.List;

/**
 * Created by zhangjiakai on 2017/7/15.
 */
public class SaveData extends HttpServlet {
    public void doGet(HttpServletRequest request, HttpServletResponse response)throws ServletException, IOException
    {
        //接收数据
        String username = request.getParameter("UserName");
        String score = request.getParameter("Score");
        String money = request.getParameter("Money");
        String level = request.getParameter("Level");
        String castle = request.getParameter("Castle");
        String defence = request.getParameter("Defence");
        String propone = request.getParameter("Propone");
        String proptwo = request.getParameter("Proptwo");
        String propthree = request.getParameter("Propthree");

        Writer out=response.getWriter();

        List<String> players = Check.check("SELECT player FROM data");
        Connection conn = null;
        try {
            conn = Source.getConnection();
            for (int i = 0;i<players.size();i++){
                if (players.get(i).equals(username)){
//                    //总分数
//                    int s = Integer.parseInt(scores.get(i)) + Integer.parseInt(score);
//                    //总钱数
//                    int m = Integer.parseInt(moneys.get(i)) + Integer.parseInt(money);
                    System.out.println(players.get(i)+":" + money + ":" + score);
                    String sql ="UPDATE data SET score ='" + score +"',money=" +"\'" + money  + "' WHERE player = '" + players.get(i) + "\'" ;
                    Statement state=conn.createStatement();
                    state.executeUpdate(sql);
                    sql ="UPDATE shop SET playerlv ='" + level +"',castlelv=" +"\'" + castle  +"',defencelv=" +"\'" + defence
                            +"',propone=" +"\'" + propone +"',proptwo=" +"\'" + proptwo +"',propthree=" +"\'" + propthree +
                            "' WHERE player = '" + players.get(i) + "\'" ;
                    System.out.println(sql);
                    state=conn.createStatement();
                    state.executeUpdate(sql);
                }
            }
            conn.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        out.write("true");
    }
}

