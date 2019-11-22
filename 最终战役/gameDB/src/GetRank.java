import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.Writer;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

/**
 * Created by zhangjiakai on 2017/7/15.
 */
public class GetRank extends HttpServlet {
    public void doGet(HttpServletRequest request, HttpServletResponse response)throws ServletException, IOException
    {
        //接收数据
        request.setCharacterEncoding("UTF-8");
        response.setContentType("text/html;charset=utf-8");
        Writer out=response.getWriter();
        List<String> scores = Check.check("SELECT score FROM data");
        Collections.sort(scores, new Comparator() {
            @Override
            public int compare(Object o1, Object o2) {
                return new Double((String) o2).compareTo(new Double((String) o1));
            }
        });
        Connection con = null;
        int count;
        if(scores.size() > 10) {
       count = 10;
       }else {
           count = scores.size();
       }
        StringBuilder rank = new StringBuilder();
        System.out.println(count);
        for (int  i = 0;i<count;i++){
            try {
                if(i ==0){
                    con = Source.getConnection();
                    Statement st = con.createStatement();
                    ResultSet rt = st.executeQuery("SELECT player FROM data WHERE score = \'" + scores.get(i) + "\'");
                    while(rt.next()){
                        String player =  rt.getString(1);
                        rank.append(player + ";");
                        rank.append(scores.get(i)+";");
                    }
                    con.close();
                }else {
                    if (!scores.get(i).equals(scores.get(i-1))){
                        con = Source.getConnection();
                        Statement st = con.createStatement();
                        ResultSet rt = st.executeQuery("SELECT player FROM data WHERE score = \'" + scores.get(i) + "\'");
                        while(rt.next()){
                            String player =  rt.getString(1);
                            rank.append(player + ";");
                            rank.append(scores.get(i)+";");
                        }
                        con.close();
                    }
                }


            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        System.out.println(rank.toString());
        out.write(rank.toString());
    }
}

