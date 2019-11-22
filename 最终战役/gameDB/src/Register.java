import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.xml.crypto.URIDereferencer;
import java.io.IOException;
import java.io.Writer;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.util.List;

/**
 * Created by zhangjiakai on 2017/7/13.
 */
public class Register extends HttpServlet {
    public void doGet(HttpServletRequest request, HttpServletResponse response)throws ServletException, IOException
    {

        String username=request.getParameter("UserName");
        String password=request.getParameter("PassWord");
        System.out.println(username+":"+password);

        Writer out=response.getWriter();
        List<String> players = Check.check("SELECT player FROM player");
        for (int i = 0;i<players.size();i++){
            if (players.get(i).equals(username)){
                out.write("false");
                return;
            }
        }
        Connection conn = null;
        try {
            conn = Source.getConnection();
            PreparedStatement st = conn.prepareStatement("INSERT INTO player(player, password) VALUES (?,?)");
            st.setString(1,username);
            st.setString(2,password);
            st.execute();
            st = conn.prepareStatement("INSERT INTO data(player, score, money) VALUES (?,?,?)");
            st.setString(1,username);
            st.setString(2,"0");
            st.setString(3,"0");
            st.execute();
            st = conn.prepareStatement("INSERT INTO shop(player, playerlv, castlelv,defencelv,propone,proptwo,propthree) VALUES (?,?,?,?,?,?,?)");
            st.setString(1,username);
            st.setString(2,"0");
            st.setString(3,"0");
            st.setString(4,"0");
            st.setString(5,"0");
            st.setString(6,"0");
            st.setString(7,"0");
            st.execute();
            conn.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        out.write("true");
    }
}

