

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.Writer;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.util.List;

/**
 * Created by zhangjiakai on 2017/7/12.
 */
public class Land extends HttpServlet{
    public void doGet(HttpServletRequest request, HttpServletResponse response)throws ServletException, IOException
    {
        String username=request.getParameter("UserName");
        String password=request.getParameter("PassWord");
        System.out.println(username+":"+password);
        Writer out=response.getWriter();
        List<String> players = Check.check("SELECT player FROM player");
        List<String> passwords = Check.check("SELECT password FROM player");
        for (int i = 0;i<players.size();i++){
            System.out.println(players.get(i)+":"+passwords.get(i));
            if (players.get(i).equals(username)&&passwords.get(i).equals(password)){
                out.write("true");
                return;
            }
        }

       out.write("false");
    }
}

