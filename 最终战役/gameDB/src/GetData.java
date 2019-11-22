import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.Writer;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.List;

/**
 * Created by zhangjiakai on 2017/7/17.
 */
public class GetData extends HttpServlet {
    public void doGet(HttpServletRequest request, HttpServletResponse response)throws ServletException, IOException
    {
        //接收数据
        request.setCharacterEncoding("UTF-8");
        response.setContentType("text/html;charset=utf-8");
        String username = request.getParameter("UserName");
        Writer out=response.getWriter();
        List<String> players = Check.check("SELECT player FROM data");
        List<String> scores = Check.check("SELECT score FROM data");
        List<String> moneys = Check.check("SELECT money FROM data");
        Connection conn = null;
        try {
            conn = Source.getConnection();
            System.out.println(players.size());
            for (int i = 0;i<players.size();i++){
                if (players.get(i).equals(username)){
                    Statement st = conn.createStatement();
                    ResultSet rt = st.executeQuery("SELECT playerlv FROM shop WHERE player = \'" + players.get(i) + "\'");
                    String playerlv="";
                    while(rt.next()){
                        playerlv =  rt.getString(1);
                    }
                    rt = st.executeQuery("SELECT castlelv FROM shop WHERE player = \'" + players.get(i) + "\'");
                    String castlelv="";
                    while(rt.next()){
                        castlelv =  rt.getString(1);
                    }
                    rt = st.executeQuery("SELECT defencelv FROM shop WHERE player = \'" + players.get(i) + "\'");
                    String defencelv="";
                    while(rt.next()){
                        defencelv =  rt.getString(1);
                    }
                    rt = st.executeQuery("SELECT propone FROM shop WHERE player = \'" + players.get(i) + "\'");
                    String propone="";
                    while(rt.next()){
                        propone =  rt.getString(1);
                    }
                    rt = st.executeQuery("SELECT proptwo FROM shop WHERE player = \'" + players.get(i) + "\'");
                    String proptwo="";
                    while(rt.next()){
                        proptwo =  rt.getString(1);
                    }
                    rt = st.executeQuery("SELECT propthree FROM shop WHERE player = \'" + players.get(i) + "\'");
                    String propthree="";
                    while(rt.next()){
                        propthree =  rt.getString(1);
                    }
                    System.out.println(players.get(i) + ";" + scores.get(i) + ";" + moneys.get(i) + ";"+
                            playerlv + ";" + castlelv + ";"+ defencelv + ";"
                            + propone + ";"+ proptwo + ";"+ propthree + ";");
                    out.write(players.get(i) + ";" + scores.get(i) + ";" + moneys.get(i) + ";"+
                            playerlv + ";" + castlelv + ";"+ defencelv + ";"
                            + propone + ";"+ proptwo + ";"+ propthree + ";");
             }
            }
            conn.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

